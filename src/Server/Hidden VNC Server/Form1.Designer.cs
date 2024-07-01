
namespace Hidden_VNC_Server
{
    partial class HVNC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HVNC));
            this.img = new System.Windows.Forms.PictureBox();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusIcon = new System.Windows.Forms.ToolStripSplitButton();
            this.stopListeningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startListeningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.settingsClick = new System.Windows.Forms.ToolStripSplitButton();
            this.settingsClickTooltip = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsHideClickTooltip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.startClick = new System.Windows.Forms.ToolStripSplitButton();
            this.runChrome = new System.Windows.Forms.ToolStripMenuItem();
            this.runCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.runPowershell = new System.Windows.Forms.ToolStripMenuItem();
            this.runExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.explorerClick = new System.Windows.Forms.ToolStripSplitButton();
            this.imageQuality = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.jpegCompression = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.processCooldown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.inputCooldown = new System.Windows.Forms.NumericUpDown();
            this.settings = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resetChanges = new System.Windows.Forms.Button();
            this.saveChanges = new System.Windows.Forms.Button();
            this.difference = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.enableDebug = new System.Windows.Forms.CheckBox();
            this.processCooldownTrack = new System.Windows.Forms.TrackBar();
            this.inputCooldownTrack = new System.Windows.Forms.TrackBar();
            this.jpegCompressionTrack = new System.Windows.Forms.TrackBar();
            this.imageQualityTrack = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.img)).BeginInit();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jpegCompression)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processCooldown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputCooldown)).BeginInit();
            this.settings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.difference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processCooldownTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputCooldownTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jpegCompressionTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageQualityTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // img
            // 
            this.img.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img.BackColor = System.Drawing.Color.Black;
            this.img.Location = new System.Drawing.Point(12, 12);
            this.img.Name = "img";
            this.img.Size = new System.Drawing.Size(709, 415);
            this.img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img.TabIndex = 0;
            this.img.TabStop = false;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.White;
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText,
            this.statusIcon,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel3,
            this.settingsClick,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel1,
            this.startClick,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel2,
            this.explorerClick});
            this.status.Location = new System.Drawing.Point(0, 442);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(733, 22);
            this.status.TabIndex = 1;
            this.status.Text = "statusStrip1";
            // 
            // statusText
            // 
            this.statusText.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusText.ForeColor = System.Drawing.Color.Black;
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(82, 17);
            this.statusText.Text = "Status: Offline";
            // 
            // statusIcon
            // 
            this.statusIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusIcon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stopListeningToolStripMenuItem,
            this.startListeningToolStripMenuItem});
            this.statusIcon.Image = ((System.Drawing.Image)(resources.GetObject("statusIcon.Image")));
            this.statusIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusIcon.Name = "statusIcon";
            this.statusIcon.Size = new System.Drawing.Size(32, 20);
            this.statusIcon.Text = "toolStripSplitButton1";
            // 
            // stopListeningToolStripMenuItem
            // 
            this.stopListeningToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.stopListeningToolStripMenuItem.Name = "stopListeningToolStripMenuItem";
            this.stopListeningToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.stopListeningToolStripMenuItem.Text = "Stop";
            this.stopListeningToolStripMenuItem.Click += new System.EventHandler(this.stopListeningToolStripMenuItem_Click);
            // 
            // startListeningToolStripMenuItem
            // 
            this.startListeningToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.startListeningToolStripMenuItem.Name = "startListeningToolStripMenuItem";
            this.startListeningToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.startListeningToolStripMenuItem.Text = "Start";
            this.startListeningToolStripMenuItem.Click += new System.EventHandler(this.startListeningToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel6.Text = "|";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabel3.Text = "Settings";
            // 
            // settingsClick
            // 
            this.settingsClick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsClick.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsClickTooltip,
            this.settingsHideClickTooltip});
            this.settingsClick.Image = ((System.Drawing.Image)(resources.GetObject("settingsClick.Image")));
            this.settingsClick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsClick.Name = "settingsClick";
            this.settingsClick.Size = new System.Drawing.Size(32, 20);
            this.settingsClick.Text = "toolStripSplitButton3";
            this.settingsClick.ButtonClick += new System.EventHandler(this.settingsClick_ButtonClick);
            // 
            // settingsClickTooltip
            // 
            this.settingsClickTooltip.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.settingsClickTooltip.Name = "settingsClickTooltip";
            this.settingsClickTooltip.Size = new System.Drawing.Size(180, 22);
            this.settingsClickTooltip.Text = "Open";
            // 
            // settingsHideClickTooltip
            // 
            this.settingsHideClickTooltip.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.settingsHideClickTooltip.Name = "settingsHideClickTooltip";
            this.settingsHideClickTooltip.Size = new System.Drawing.Size(180, 22);
            this.settingsHideClickTooltip.Text = "Collapse";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel5.Text = "|";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "Start";
            // 
            // startClick
            // 
            this.startClick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startClick.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runChrome,
            this.runCmd,
            this.runPowershell,
            this.runExplorer,
            this.toolStripSeparator1,
            this.runCustom});
            this.startClick.Enabled = false;
            this.startClick.Image = ((System.Drawing.Image)(resources.GetObject("startClick.Image")));
            this.startClick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startClick.Name = "startClick";
            this.startClick.Size = new System.Drawing.Size(32, 20);
            this.startClick.Text = "toolStripSplitButton1";
            // 
            // runChrome
            // 
            this.runChrome.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.runChrome.Image = ((System.Drawing.Image)(resources.GetObject("runChrome.Image")));
            this.runChrome.Name = "runChrome";
            this.runChrome.Size = new System.Drawing.Size(180, 22);
            this.runChrome.Text = "Chrome";
            // 
            // runCmd
            // 
            this.runCmd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.runCmd.Image = ((System.Drawing.Image)(resources.GetObject("runCmd.Image")));
            this.runCmd.Name = "runCmd";
            this.runCmd.Size = new System.Drawing.Size(180, 22);
            this.runCmd.Text = "Cmd";
            // 
            // runPowershell
            // 
            this.runPowershell.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.runPowershell.Image = ((System.Drawing.Image)(resources.GetObject("runPowershell.Image")));
            this.runPowershell.Name = "runPowershell";
            this.runPowershell.Size = new System.Drawing.Size(180, 22);
            this.runPowershell.Text = "Powershell";
            // 
            // runExplorer
            // 
            this.runExplorer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.runExplorer.Image = ((System.Drawing.Image)(resources.GetObject("runExplorer.Image")));
            this.runExplorer.Name = "runExplorer";
            this.runExplorer.Size = new System.Drawing.Size(180, 22);
            this.runExplorer.Text = "Explorer";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // runCustom
            // 
            this.runCustom.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.runCustom.Image = ((System.Drawing.Image)(resources.GetObject("runCustom.Image")));
            this.runCustom.Name = "runCustom";
            this.runCustom.Size = new System.Drawing.Size(180, 22);
            this.runCustom.Text = "Custom";
            this.runCustom.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel2.Text = "End Explorer";
            // 
            // explorerClick
            // 
            this.explorerClick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.explorerClick.Enabled = false;
            this.explorerClick.Image = ((System.Drawing.Image)(resources.GetObject("explorerClick.Image")));
            this.explorerClick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.explorerClick.Name = "explorerClick";
            this.explorerClick.Size = new System.Drawing.Size(32, 20);
            this.explorerClick.Text = "explorerClick";
            this.explorerClick.ButtonClick += new System.EventHandler(this.toolStripSplitButton2_ButtonClick);
            // 
            // imageQuality
            // 
            this.imageQuality.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.imageQuality.Location = new System.Drawing.Point(84, 33);
            this.imageQuality.Name = "imageQuality";
            this.imageQuality.Size = new System.Drawing.Size(92, 22);
            this.imageQuality.TabIndex = 2;
            this.imageQuality.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Image Quality";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "JPEG Compression";
            // 
            // jpegCompression
            // 
            this.jpegCompression.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.jpegCompression.Location = new System.Drawing.Point(84, 72);
            this.jpegCompression.Name = "jpegCompression";
            this.jpegCompression.Size = new System.Drawing.Size(92, 22);
            this.jpegCompression.TabIndex = 4;
            this.jpegCompression.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Process Cooldown";
            // 
            // processCooldown
            // 
            this.processCooldown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.processCooldown.Location = new System.Drawing.Point(84, 152);
            this.processCooldown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.processCooldown.Name = "processCooldown";
            this.processCooldown.Size = new System.Drawing.Size(92, 22);
            this.processCooldown.TabIndex = 10;
            this.processCooldown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Input Cooldown";
            // 
            // inputCooldown
            // 
            this.inputCooldown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.inputCooldown.Location = new System.Drawing.Point(84, 111);
            this.inputCooldown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.inputCooldown.Name = "inputCooldown";
            this.inputCooldown.Size = new System.Drawing.Size(92, 22);
            this.inputCooldown.TabIndex = 8;
            this.inputCooldown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // settings
            // 
            this.settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settings.Controls.Add(this.groupBox2);
            this.settings.Controls.Add(this.processCooldownTrack);
            this.settings.Controls.Add(this.inputCooldownTrack);
            this.settings.Controls.Add(this.jpegCompressionTrack);
            this.settings.Controls.Add(this.imageQualityTrack);
            this.settings.Controls.Add(this.label4);
            this.settings.Controls.Add(this.processCooldown);
            this.settings.Controls.Add(this.label5);
            this.settings.Controls.Add(this.inputCooldown);
            this.settings.Controls.Add(this.label2);
            this.settings.Controls.Add(this.jpegCompression);
            this.settings.Controls.Add(this.label1);
            this.settings.Controls.Add(this.imageQuality);
            this.settings.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.settings.Location = new System.Drawing.Point(12, 239);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(709, 188);
            this.settings.TabIndex = 12;
            this.settings.TabStop = false;
            this.settings.Text = "Settings";
            this.settings.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox2.Controls.Add(this.resetChanges);
            this.groupBox2.Controls.Add(this.saveChanges);
            this.groupBox2.Controls.Add(this.difference);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.enableDebug);
            this.groupBox2.Location = new System.Drawing.Point(384, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 152);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuration";
            // 
            // resetChanges
            // 
            this.resetChanges.Location = new System.Drawing.Point(127, 109);
            this.resetChanges.Name = "resetChanges";
            this.resetChanges.Size = new System.Drawing.Size(92, 28);
            this.resetChanges.TabIndex = 13;
            this.resetChanges.Text = "Reset";
            this.resetChanges.UseVisualStyleBackColor = true;
            this.resetChanges.Click += new System.EventHandler(this.resetChanges_Click);
            // 
            // saveChanges
            // 
            this.saveChanges.Location = new System.Drawing.Point(23, 109);
            this.saveChanges.Name = "saveChanges";
            this.saveChanges.Size = new System.Drawing.Size(92, 28);
            this.saveChanges.TabIndex = 12;
            this.saveChanges.Text = "Save";
            this.saveChanges.UseVisualStyleBackColor = true;
            this.saveChanges.Click += new System.EventHandler(this.saveChanges_Click);
            // 
            // difference
            // 
            this.difference.DecimalPlaces = 10;
            this.difference.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.difference.Location = new System.Drawing.Point(23, 48);
            this.difference.Name = "difference";
            this.difference.Size = new System.Drawing.Size(196, 22);
            this.difference.TabIndex = 11;
            this.difference.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Difference Treshold";
            // 
            // enableDebug
            // 
            this.enableDebug.AutoSize = true;
            this.enableDebug.Checked = true;
            this.enableDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableDebug.Location = new System.Drawing.Point(23, 73);
            this.enableDebug.Name = "enableDebug";
            this.enableDebug.Size = new System.Drawing.Size(123, 17);
            this.enableDebug.TabIndex = 8;
            this.enableDebug.Text = "Enable debug logs";
            this.enableDebug.UseVisualStyleBackColor = true;
            // 
            // processCooldownTrack
            // 
            this.processCooldownTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.processCooldownTrack.Location = new System.Drawing.Point(182, 137);
            this.processCooldownTrack.Maximum = 1000;
            this.processCooldownTrack.Name = "processCooldownTrack";
            this.processCooldownTrack.Size = new System.Drawing.Size(195, 45);
            this.processCooldownTrack.TabIndex = 15;
            this.processCooldownTrack.Value = 1;
            // 
            // inputCooldownTrack
            // 
            this.inputCooldownTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.inputCooldownTrack.LargeChange = 10;
            this.inputCooldownTrack.Location = new System.Drawing.Point(182, 95);
            this.inputCooldownTrack.Maximum = 500;
            this.inputCooldownTrack.Name = "inputCooldownTrack";
            this.inputCooldownTrack.Size = new System.Drawing.Size(195, 45);
            this.inputCooldownTrack.TabIndex = 14;
            this.inputCooldownTrack.Value = 1;
            // 
            // jpegCompressionTrack
            // 
            this.jpegCompressionTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.jpegCompressionTrack.Location = new System.Drawing.Point(182, 63);
            this.jpegCompressionTrack.Maximum = 100;
            this.jpegCompressionTrack.Name = "jpegCompressionTrack";
            this.jpegCompressionTrack.Size = new System.Drawing.Size(195, 45);
            this.jpegCompressionTrack.TabIndex = 13;
            this.jpegCompressionTrack.Value = 50;
            // 
            // imageQualityTrack
            // 
            this.imageQualityTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.imageQualityTrack.Location = new System.Drawing.Point(182, 24);
            this.imageQualityTrack.Maximum = 100;
            this.imageQualityTrack.Name = "imageQualityTrack";
            this.imageQualityTrack.Size = new System.Drawing.Size(195, 45);
            this.imageQualityTrack.TabIndex = 12;
            this.imageQualityTrack.Value = 70;
            // 
            // HVNC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(733, 464);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.status);
            this.Controls.Add(this.img);
            this.MinimumSize = new System.Drawing.Size(749, 503);
            this.Name = "HVNC";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hidden VNC | Concept from github.com/ntdll0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.img)).EndInit();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jpegCompression)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processCooldown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputCooldown)).EndInit();
            this.settings.ResumeLayout(false);
            this.settings.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.difference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processCooldownTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputCooldownTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jpegCompressionTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageQualityTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox img;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripSplitButton statusIcon;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.ToolStripMenuItem stopListeningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startListeningToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton startClick;
        private System.Windows.Forms.ToolStripMenuItem runChrome;
        private System.Windows.Forms.ToolStripMenuItem runCmd;
        private System.Windows.Forms.ToolStripMenuItem runPowershell;
        private System.Windows.Forms.ToolStripMenuItem runExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem runCustom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripSplitButton explorerClick;
        private System.Windows.Forms.NumericUpDown imageQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown jpegCompression;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown processCooldown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown inputCooldown;
        private System.Windows.Forms.GroupBox settings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox enableDebug;
        private System.Windows.Forms.TrackBar processCooldownTrack;
        private System.Windows.Forms.TrackBar inputCooldownTrack;
        private System.Windows.Forms.TrackBar jpegCompressionTrack;
        private System.Windows.Forms.TrackBar imageQualityTrack;
        private System.Windows.Forms.NumericUpDown difference;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button resetChanges;
        private System.Windows.Forms.Button saveChanges;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripSplitButton settingsClick;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripMenuItem settingsClickTooltip;
        private System.Windows.Forms.ToolStripMenuItem settingsHideClickTooltip;
    }
}

