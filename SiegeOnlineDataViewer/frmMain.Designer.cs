using System.Windows.Forms;
using SiegeOnlineDataViewer.FormControls;

namespace SiegeOnlineDataViewer
{
	partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.statText = new System.Windows.Forms.ToolStripStatusLabel();
			this.statProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tabBuildings = new System.Windows.Forms.TabPage();
			this.splitContainerBuildingsMain = new System.Windows.Forms.SplitContainer();
			this.chbxFilterBuildingAutoUpdate = new System.Windows.Forms.CheckBox();
			this.btnFilterBuildingApply = new System.Windows.Forms.Button();
			this.btnFilterBuildingReset = new System.Windows.Forms.Button();
			this.cmbbxFilterBuildingSizeMode = new System.Windows.Forms.ComboBox();
			this.numFilterBuildingSizeB = new System.Windows.Forms.NumericUpDown();
			this.numFilterBuildingLevelMin = new System.Windows.Forms.NumericUpDown();
			this.numFilterBuildingLevelMax = new System.Windows.Forms.NumericUpDown();
			this.numFilterBuildingSizeA = new System.Windows.Forms.NumericUpDown();
			this.txtbxFilterBuildingCode = new System.Windows.Forms.TextBox();
			this.txtbxFilterBuildingName = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.splitContainerBuildingsInner = new System.Windows.Forms.SplitContainer();
			this.dataGridBuildings = new System.Windows.Forms.DataGridView();
			this.lblBuildingTitle = new System.Windows.Forms.TextBox();
			this.lblBuildingCode = new System.Windows.Forms.TextBox();
			this.tabsBuildingInfo = new System.Windows.Forms.TabControl();
			this.tabBuildingRequirements = new System.Windows.Forms.TabPage();
			this.tabBuildingAfterBuild = new System.Windows.Forms.TabPage();
			this.tabBuildingConstruction = new System.Windows.Forms.TabPage();
			this.lblBuildingConstTime = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.tabBuildingProducts = new System.Windows.Forms.TabPage();
			this.lblBuildingProductionTime = new System.Windows.Forms.Label();
			this.lblBuildingProductionType = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.tabBuildingUpgrade = new System.Windows.Forms.TabPage();
			this.lblBuildingUpgradeMinLevel = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.pictBuilding = new System.Windows.Forms.PictureBox();
			this.lblBuildingSize = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tabStart = new System.Windows.Forms.TabPage();
			this.btnBasePath = new System.Windows.Forms.Button();
			this.lblDonate = new System.Windows.Forms.Label();
			this.richTextBoxAbout = new System.Windows.Forms.RichTextBox();
			this.linkSiege = new System.Windows.Forms.LinkLabel();
			this.listBoxLanguages = new System.Windows.Forms.ListBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tbxBasePath = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabsMain = new System.Windows.Forms.TabControl();
			this.tabSquads = new System.Windows.Forms.TabPage();
			this.splitContainerSquadMain = new System.Windows.Forms.SplitContainer();
			this.chlstFilterSquadTypes = new System.Windows.Forms.CheckedListBox();
			this.chbxFilterSquadAutoUpdate = new System.Windows.Forms.CheckBox();
			this.btnFilterSquadApply = new System.Windows.Forms.Button();
			this.btnFilterSquadReset = new System.Windows.Forms.Button();
			this.numFilterSquadLevelMin = new System.Windows.Forms.NumericUpDown();
			this.numFilterSquadLevelMax = new System.Windows.Forms.NumericUpDown();
			this.txtbxFilterSquadCode = new System.Windows.Forms.TextBox();
			this.txtbxFilterSquadName = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.splitContainerSquadInner = new System.Windows.Forms.SplitContainer();
			this.dataGridSquads = new System.Windows.Forms.DataGridView();
			this.linkSquadBuilding = new System.Windows.Forms.LinkLabel();
			this.txtbxSquadMainDescription = new System.Windows.Forms.TextBox();
			this.tabsSquadsInfo = new System.Windows.Forms.TabControl();
			this.tabSquadCommon = new System.Windows.Forms.TabPage();
			this.txtbxSquadBattleDesc = new System.Windows.Forms.TextBox();
			this.tabSquadTTH = new System.Windows.Forms.TabPage();
			this.txtbxSquadTTH = new System.Windows.Forms.TextBox();
			this.tabSquadUpgrade = new System.Windows.Forms.TabPage();
			this.txtbxSquadUpgradeInfo = new System.Windows.Forms.TextBox();
			this.tabSquadCalc = new System.Windows.Forms.TabPage();
			this.grpbxSquadCalc = new System.Windows.Forms.GroupBox();
			this.label26 = new System.Windows.Forms.Label();
			this.lblSquadCalcSell = new System.Windows.Forms.Label();
			this.lblSquadCalcBuy = new System.Windows.Forms.Label();
			this.numSquadCalcPack = new System.Windows.Forms.NumericUpDown();
			this.chbxSquadCalcLastLevels = new System.Windows.Forms.CheckBox();
			this.numSquadCalcTryDiamonds = new System.Windows.Forms.NumericUpDown();
			this.numSquadCalcTryCounts = new System.Windows.Forms.NumericUpDown();
			this.numSquadCalcLevel = new System.Windows.Forms.NumericUpDown();
			this.label25 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.txtbxSquadCalc = new System.Windows.Forms.TextBox();
			this.lblSquadTitle = new System.Windows.Forms.TextBox();
			this.pictSquad = new System.Windows.Forms.PictureBox();
			this.lblSquadCode = new System.Windows.Forms.TextBox();
			this.tabQuest = new System.Windows.Forms.TabPage();
			this.splitContainerQuestMain = new System.Windows.Forms.SplitContainer();
			this.label8 = new System.Windows.Forms.Label();
			this.chlstFilterQuestTypes = new System.Windows.Forms.CheckedListBox();
			this.chbxFilterQuestAutoUpdate = new System.Windows.Forms.CheckBox();
			this.btnFilterQuestApply = new System.Windows.Forms.Button();
			this.btnFilterQuestReset = new System.Windows.Forms.Button();
			this.numFilterQuestLevelMin = new System.Windows.Forms.NumericUpDown();
			this.numFilterQuestLevelMax = new System.Windows.Forms.NumericUpDown();
			this.txtbxFilterQuestCode = new System.Windows.Forms.TextBox();
			this.txtbxFilterQuestName = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.splitContainerQuestInner = new System.Windows.Forms.SplitContainer();
			this.dataGridQuests = new System.Windows.Forms.DataGridView();
			this.txtbxQuest = new SiegeOnlineDataViewer.FormControls.RichTextBoxEx();
			this.lblQuestCode = new System.Windows.Forms.TextBox();
			this.chbxIconSize = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.statusBar.SuspendLayout();
			this.tabBuildings.SuspendLayout();
			this.splitContainerBuildingsMain.Panel1.SuspendLayout();
			this.splitContainerBuildingsMain.Panel2.SuspendLayout();
			this.splitContainerBuildingsMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingSizeB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingLevelMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingLevelMax)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingSizeA)).BeginInit();
			this.splitContainerBuildingsInner.Panel1.SuspendLayout();
			this.splitContainerBuildingsInner.Panel2.SuspendLayout();
			this.splitContainerBuildingsInner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridBuildings)).BeginInit();
			this.tabsBuildingInfo.SuspendLayout();
			this.tabBuildingConstruction.SuspendLayout();
			this.tabBuildingProducts.SuspendLayout();
			this.tabBuildingUpgrade.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictBuilding)).BeginInit();
			this.tabStart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabsMain.SuspendLayout();
			this.tabSquads.SuspendLayout();
			this.splitContainerSquadMain.Panel1.SuspendLayout();
			this.splitContainerSquadMain.Panel2.SuspendLayout();
			this.splitContainerSquadMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFilterSquadLevelMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterSquadLevelMax)).BeginInit();
			this.splitContainerSquadInner.Panel1.SuspendLayout();
			this.splitContainerSquadInner.Panel2.SuspendLayout();
			this.splitContainerSquadInner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridSquads)).BeginInit();
			this.tabsSquadsInfo.SuspendLayout();
			this.tabSquadCommon.SuspendLayout();
			this.tabSquadTTH.SuspendLayout();
			this.tabSquadUpgrade.SuspendLayout();
			this.tabSquadCalc.SuspendLayout();
			this.grpbxSquadCalc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcPack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcTryDiamonds)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcTryCounts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictSquad)).BeginInit();
			this.tabQuest.SuspendLayout();
			this.splitContainerQuestMain.Panel1.SuspendLayout();
			this.splitContainerQuestMain.Panel2.SuspendLayout();
			this.splitContainerQuestMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFilterQuestLevelMin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterQuestLevelMax)).BeginInit();
			this.splitContainerQuestInner.Panel1.SuspendLayout();
			this.splitContainerQuestInner.Panel2.SuspendLayout();
			this.splitContainerQuestInner.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridQuests)).BeginInit();
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statText,
            this.statProgressBar});
			this.statusBar.Location = new System.Drawing.Point(0, 444);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(754, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "statusStrip1";
			// 
			// statText
			// 
			this.statText.Name = "statText";
			this.statText.Size = new System.Drawing.Size(186, 17);
			this.statText.Tag = "Осада Онлайн (www.siegeonline.ru)";
			this.statText.Text = "Осада Онлайн (www.siegeonline.ru)";
			// 
			// statProgressBar
			// 
			this.statProgressBar.Name = "statProgressBar";
			this.statProgressBar.Size = new System.Drawing.Size(400, 16);
			this.statProgressBar.Visible = false;
			// 
			// tabBuildings
			// 
			this.tabBuildings.BackColor = System.Drawing.SystemColors.Control;
			this.tabBuildings.Controls.Add(this.splitContainerBuildingsMain);
			this.tabBuildings.Location = new System.Drawing.Point(4, 22);
			this.tabBuildings.Name = "tabBuildings";
			this.tabBuildings.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuildings.Size = new System.Drawing.Size(746, 415);
			this.tabBuildings.TabIndex = 0;
			this.tabBuildings.Text = "Здания";
			this.tabBuildings.ToolTipText = "Постройки, которые можно возвести в замке";
			// 
			// splitContainerBuildingsMain
			// 
			this.splitContainerBuildingsMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerBuildingsMain.Location = new System.Drawing.Point(3, 3);
			this.splitContainerBuildingsMain.Name = "splitContainerBuildingsMain";
			// 
			// splitContainerBuildingsMain.Panel1
			// 
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.chbxFilterBuildingAutoUpdate);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.btnFilterBuildingApply);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.btnFilterBuildingReset);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.cmbbxFilterBuildingSizeMode);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.numFilterBuildingSizeB);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.numFilterBuildingLevelMin);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.numFilterBuildingLevelMax);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.numFilterBuildingSizeA);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.txtbxFilterBuildingCode);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.txtbxFilterBuildingName);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label15);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label6);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label2);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label5);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label11);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label4);
			this.splitContainerBuildingsMain.Panel1.Controls.Add(this.label3);
			this.splitContainerBuildingsMain.Panel1MinSize = 160;
			// 
			// splitContainerBuildingsMain.Panel2
			// 
			this.splitContainerBuildingsMain.Panel2.Controls.Add(this.splitContainerBuildingsInner);
			this.splitContainerBuildingsMain.Size = new System.Drawing.Size(740, 409);
			this.splitContainerBuildingsMain.SplitterDistance = 160;
			this.splitContainerBuildingsMain.TabIndex = 0;
			// 
			// chbxFilterBuildingAutoUpdate
			// 
			this.chbxFilterBuildingAutoUpdate.AutoSize = true;
			this.chbxFilterBuildingAutoUpdate.Checked = true;
			this.chbxFilterBuildingAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbxFilterBuildingAutoUpdate.Location = new System.Drawing.Point(5, 198);
			this.chbxFilterBuildingAutoUpdate.Name = "chbxFilterBuildingAutoUpdate";
			this.chbxFilterBuildingAutoUpdate.Size = new System.Drawing.Size(85, 17);
			this.chbxFilterBuildingAutoUpdate.TabIndex = 6;
			this.chbxFilterBuildingAutoUpdate.Text = "Автоапдейт";
			this.chbxFilterBuildingAutoUpdate.UseVisualStyleBackColor = true;
			this.chbxFilterBuildingAutoUpdate.CheckedChanged += new System.EventHandler(this.chbxFilterAutoUpdate_CheckedChanged);
			// 
			// btnFilterBuildingApply
			// 
			this.btnFilterBuildingApply.Enabled = false;
			this.btnFilterBuildingApply.Location = new System.Drawing.Point(5, 221);
			this.btnFilterBuildingApply.Name = "btnFilterBuildingApply";
			this.btnFilterBuildingApply.Size = new System.Drawing.Size(75, 24);
			this.btnFilterBuildingApply.TabIndex = 5;
			this.btnFilterBuildingApply.Text = "Применить";
			this.btnFilterBuildingApply.UseVisualStyleBackColor = true;
			this.btnFilterBuildingApply.Click += new System.EventHandler(this.btnFilterApply_Click);
			// 
			// btnFilterBuildingReset
			// 
			this.btnFilterBuildingReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFilterBuildingReset.Location = new System.Drawing.Point(80, 221);
			this.btnFilterBuildingReset.Name = "btnFilterBuildingReset";
			this.btnFilterBuildingReset.Size = new System.Drawing.Size(75, 24);
			this.btnFilterBuildingReset.TabIndex = 5;
			this.btnFilterBuildingReset.Text = "Сбросить";
			this.btnFilterBuildingReset.UseVisualStyleBackColor = true;
			this.btnFilterBuildingReset.Click += new System.EventHandler(this.btnFilterReset_Click);
			// 
			// cmbbxFilterBuildingSizeMode
			// 
			this.cmbbxFilterBuildingSizeMode.FormattingEnabled = true;
			this.cmbbxFilterBuildingSizeMode.Items.AddRange(new object[] {
            "<",
            "<=",
            "=",
            ">=",
            ">"});
			this.cmbbxFilterBuildingSizeMode.Location = new System.Drawing.Point(5, 120);
			this.cmbbxFilterBuildingSizeMode.Name = "cmbbxFilterBuildingSizeMode";
			this.cmbbxFilterBuildingSizeMode.Size = new System.Drawing.Size(38, 21);
			this.cmbbxFilterBuildingSizeMode.TabIndex = 2;
			this.cmbbxFilterBuildingSizeMode.Tag = "SizeCondition";
			this.cmbbxFilterBuildingSizeMode.Text = ">=";
			this.cmbbxFilterBuildingSizeMode.SelectedIndexChanged += new System.EventHandler(this.cmbbxFilter_SelectedIndexChanged);
			this.cmbbxFilterBuildingSizeMode.Leave += new System.EventHandler(this.cmbbxFilter_Leave);
			// 
			// numFilterBuildingSizeB
			// 
			this.numFilterBuildingSizeB.Location = new System.Drawing.Point(98, 120);
			this.numFilterBuildingSizeB.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numFilterBuildingSizeB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingSizeB.Name = "numFilterBuildingSizeB";
			this.numFilterBuildingSizeB.Size = new System.Drawing.Size(36, 20);
			this.numFilterBuildingSizeB.TabIndex = 4;
			this.numFilterBuildingSizeB.Tag = "SizeB";
			this.numFilterBuildingSizeB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFilterBuildingSizeB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingSizeB.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// numFilterBuildingLevelMin
			// 
			this.numFilterBuildingLevelMin.Location = new System.Drawing.Point(5, 161);
			this.numFilterBuildingLevelMin.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterBuildingLevelMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingLevelMin.Name = "numFilterBuildingLevelMin";
			this.numFilterBuildingLevelMin.Size = new System.Drawing.Size(38, 20);
			this.numFilterBuildingLevelMin.TabIndex = 3;
			this.numFilterBuildingLevelMin.Tag = "LevelMin";
			this.numFilterBuildingLevelMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFilterBuildingLevelMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingLevelMin.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// numFilterBuildingLevelMax
			// 
			this.numFilterBuildingLevelMax.Location = new System.Drawing.Point(60, 160);
			this.numFilterBuildingLevelMax.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterBuildingLevelMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingLevelMax.Name = "numFilterBuildingLevelMax";
			this.numFilterBuildingLevelMax.Size = new System.Drawing.Size(38, 20);
			this.numFilterBuildingLevelMax.TabIndex = 3;
			this.numFilterBuildingLevelMax.Tag = "LevelMax";
			this.numFilterBuildingLevelMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFilterBuildingLevelMax.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterBuildingLevelMax.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// numFilterBuildingSizeA
			// 
			this.numFilterBuildingSizeA.Location = new System.Drawing.Point(49, 120);
			this.numFilterBuildingSizeA.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numFilterBuildingSizeA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingSizeA.Name = "numFilterBuildingSizeA";
			this.numFilterBuildingSizeA.Size = new System.Drawing.Size(36, 20);
			this.numFilterBuildingSizeA.TabIndex = 3;
			this.numFilterBuildingSizeA.Tag = "SizeA";
			this.numFilterBuildingSizeA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFilterBuildingSizeA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFilterBuildingSizeA.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// txtbxFilterBuildingCode
			// 
			this.txtbxFilterBuildingCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFilterBuildingCode.Location = new System.Drawing.Point(5, 81);
			this.txtbxFilterBuildingCode.Name = "txtbxFilterBuildingCode";
			this.txtbxFilterBuildingCode.Size = new System.Drawing.Size(151, 20);
			this.txtbxFilterBuildingCode.TabIndex = 1;
			this.txtbxFilterBuildingCode.Tag = "MaskCode";
			this.txtbxFilterBuildingCode.TextChanged += new System.EventHandler(this.txtbxFilter_TextChanged);
			// 
			// txtbxFilterBuildingName
			// 
			this.txtbxFilterBuildingName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFilterBuildingName.Location = new System.Drawing.Point(5, 42);
			this.txtbxFilterBuildingName.Name = "txtbxFilterBuildingName";
			this.txtbxFilterBuildingName.Size = new System.Drawing.Size(151, 20);
			this.txtbxFilterBuildingName.TabIndex = 0;
			this.txtbxFilterBuildingName.Tag = "MaskName";
			this.txtbxFilterBuildingName.TextChanged += new System.EventHandler(this.txtbxFilter_TextChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(46, 163);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(10, 13);
			this.label15.TabIndex = 4;
			this.label15.Text = "-";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(86, 123);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(12, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "x";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(2, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Маска по уровню:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(2, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Маска размеров:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(2, 65);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(84, 13);
			this.label11.TabIndex = 1;
			this.label11.Text = "Маска по коду:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(2, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Маска имени:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(2, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(155, 14);
			this.label3.TabIndex = 0;
			this.label3.Text = "Фильтр поиска";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// splitContainerBuildingsInner
			// 
			this.splitContainerBuildingsInner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerBuildingsInner.Location = new System.Drawing.Point(0, 0);
			this.splitContainerBuildingsInner.Name = "splitContainerBuildingsInner";
			// 
			// splitContainerBuildingsInner.Panel1
			// 
			this.splitContainerBuildingsInner.Panel1.Controls.Add(this.dataGridBuildings);
			this.splitContainerBuildingsInner.Panel1MinSize = 160;
			// 
			// splitContainerBuildingsInner.Panel2
			// 
			this.splitContainerBuildingsInner.Panel2.Controls.Add(this.lblBuildingTitle);
			this.splitContainerBuildingsInner.Panel2.Controls.Add(this.lblBuildingCode);
			this.splitContainerBuildingsInner.Panel2.Controls.Add(this.tabsBuildingInfo);
			this.splitContainerBuildingsInner.Panel2.Controls.Add(this.pictBuilding);
			this.splitContainerBuildingsInner.Panel2.Controls.Add(this.lblBuildingSize);
			this.splitContainerBuildingsInner.Panel2.Controls.Add(this.label7);
			this.splitContainerBuildingsInner.Size = new System.Drawing.Size(576, 409);
			this.splitContainerBuildingsInner.SplitterDistance = 243;
			this.splitContainerBuildingsInner.TabIndex = 0;
			// 
			// dataGridBuildings
			// 
			this.dataGridBuildings.AllowUserToAddRows = false;
			this.dataGridBuildings.AllowUserToDeleteRows = false;
			this.dataGridBuildings.AllowUserToOrderColumns = true;
			this.dataGridBuildings.AllowUserToResizeRows = false;
			this.dataGridBuildings.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridBuildings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridBuildings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridBuildings.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridBuildings.Location = new System.Drawing.Point(0, 0);
			this.dataGridBuildings.MultiSelect = false;
			this.dataGridBuildings.Name = "dataGridBuildings";
			this.dataGridBuildings.ReadOnly = true;
			this.dataGridBuildings.RowHeadersVisible = false;
			this.dataGridBuildings.RowHeadersWidth = 45;
			this.dataGridBuildings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridBuildings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridBuildings.Size = new System.Drawing.Size(243, 409);
			this.dataGridBuildings.TabIndex = 0;
			this.dataGridBuildings.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGrid_RowPrePaint);
			this.dataGridBuildings.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGrid_DataBindingComplete);
			this.dataGridBuildings.Resize += new System.EventHandler(this.dataGrid_Resize);
			this.dataGridBuildings.SelectionChanged += new System.EventHandler(this.dataGrid_SelectionChanged);
			// 
			// lblBuildingTitle
			// 
			this.lblBuildingTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblBuildingTitle.BackColor = System.Drawing.SystemColors.Control;
			this.lblBuildingTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblBuildingTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblBuildingTitle.Location = new System.Drawing.Point(0, 0);
			this.lblBuildingTitle.Name = "lblBuildingTitle";
			this.lblBuildingTitle.ReadOnly = true;
			this.lblBuildingTitle.Size = new System.Drawing.Size(329, 16);
			this.lblBuildingTitle.TabIndex = 3;
			this.lblBuildingTitle.TabStop = false;
			this.lblBuildingTitle.Text = "{code}";
			// 
			// lblBuildingCode
			// 
			this.lblBuildingCode.BackColor = System.Drawing.SystemColors.Control;
			this.lblBuildingCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblBuildingCode.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblBuildingCode.Location = new System.Drawing.Point(0, 396);
			this.lblBuildingCode.Name = "lblBuildingCode";
			this.lblBuildingCode.ReadOnly = true;
			this.lblBuildingCode.Size = new System.Drawing.Size(329, 13);
			this.lblBuildingCode.TabIndex = 3;
			this.lblBuildingCode.TabStop = false;
			this.lblBuildingCode.Text = "{code}";
			// 
			// tabsBuildingInfo
			// 
			this.tabsBuildingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabsBuildingInfo.Controls.Add(this.tabBuildingRequirements);
			this.tabsBuildingInfo.Controls.Add(this.tabBuildingAfterBuild);
			this.tabsBuildingInfo.Controls.Add(this.tabBuildingConstruction);
			this.tabsBuildingInfo.Controls.Add(this.tabBuildingProducts);
			this.tabsBuildingInfo.Controls.Add(this.tabBuildingUpgrade);
			this.tabsBuildingInfo.Location = new System.Drawing.Point(3, 156);
			this.tabsBuildingInfo.Name = "tabsBuildingInfo";
			this.tabsBuildingInfo.SelectedIndex = 0;
			this.tabsBuildingInfo.Size = new System.Drawing.Size(323, 238);
			this.tabsBuildingInfo.TabIndex = 2;
			this.tabsBuildingInfo.SelectedIndexChanged += new System.EventHandler(this.tabsInfo_SelectedIndexChanged);
			// 
			// tabBuildingRequirements
			// 
			this.tabBuildingRequirements.AutoScroll = true;
			this.tabBuildingRequirements.Location = new System.Drawing.Point(4, 22);
			this.tabBuildingRequirements.Name = "tabBuildingRequirements";
			this.tabBuildingRequirements.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuildingRequirements.Size = new System.Drawing.Size(315, 212);
			this.tabBuildingRequirements.TabIndex = 0;
			this.tabBuildingRequirements.Text = "Требования";
			this.tabBuildingRequirements.UseVisualStyleBackColor = true;
			// 
			// tabBuildingAfterBuild
			// 
			this.tabBuildingAfterBuild.AutoScroll = true;
			this.tabBuildingAfterBuild.Location = new System.Drawing.Point(4, 22);
			this.tabBuildingAfterBuild.Name = "tabBuildingAfterBuild";
			this.tabBuildingAfterBuild.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuildingAfterBuild.Size = new System.Drawing.Size(315, 212);
			this.tabBuildingAfterBuild.TabIndex = 1;
			this.tabBuildingAfterBuild.Text = "Доступно";
			this.tabBuildingAfterBuild.UseVisualStyleBackColor = true;
			// 
			// tabBuildingConstruction
			// 
			this.tabBuildingConstruction.AutoScroll = true;
			this.tabBuildingConstruction.Controls.Add(this.lblBuildingConstTime);
			this.tabBuildingConstruction.Controls.Add(this.label12);
			this.tabBuildingConstruction.Location = new System.Drawing.Point(4, 22);
			this.tabBuildingConstruction.Name = "tabBuildingConstruction";
			this.tabBuildingConstruction.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuildingConstruction.Size = new System.Drawing.Size(315, 212);
			this.tabBuildingConstruction.TabIndex = 2;
			this.tabBuildingConstruction.Text = "Стоимость";
			this.tabBuildingConstruction.UseVisualStyleBackColor = true;
			// 
			// lblBuildingConstTime
			// 
			this.lblBuildingConstTime.AutoSize = true;
			this.lblBuildingConstTime.Location = new System.Drawing.Point(122, 3);
			this.lblBuildingConstTime.Name = "lblBuildingConstTime";
			this.lblBuildingConstTime.Size = new System.Drawing.Size(13, 13);
			this.lblBuildingConstTime.TabIndex = 1;
			this.lblBuildingConstTime.Text = "0";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(3, 3);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(121, 13);
			this.label12.TabIndex = 0;
			this.label12.Text = "Время строительства:";
			// 
			// tabBuildingProducts
			// 
			this.tabBuildingProducts.AutoScroll = true;
			this.tabBuildingProducts.Controls.Add(this.lblBuildingProductionTime);
			this.tabBuildingProducts.Controls.Add(this.lblBuildingProductionType);
			this.tabBuildingProducts.Controls.Add(this.label13);
			this.tabBuildingProducts.Controls.Add(this.label14);
			this.tabBuildingProducts.Location = new System.Drawing.Point(4, 22);
			this.tabBuildingProducts.Name = "tabBuildingProducts";
			this.tabBuildingProducts.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuildingProducts.Size = new System.Drawing.Size(315, 212);
			this.tabBuildingProducts.TabIndex = 3;
			this.tabBuildingProducts.Text = "Продукция";
			this.tabBuildingProducts.UseVisualStyleBackColor = true;
			// 
			// lblBuildingProductionTime
			// 
			this.lblBuildingProductionTime.AutoSize = true;
			this.lblBuildingProductionTime.Location = new System.Drawing.Point(119, 18);
			this.lblBuildingProductionTime.Name = "lblBuildingProductionTime";
			this.lblBuildingProductionTime.Size = new System.Drawing.Size(13, 13);
			this.lblBuildingProductionTime.TabIndex = 3;
			this.lblBuildingProductionTime.Text = "0";
			// 
			// lblBuildingProductionType
			// 
			this.lblBuildingProductionType.AutoSize = true;
			this.lblBuildingProductionType.Location = new System.Drawing.Point(86, 3);
			this.lblBuildingProductionType.Name = "lblBuildingProductionType";
			this.lblBuildingProductionType.Size = new System.Drawing.Size(13, 13);
			this.lblBuildingProductionType.TabIndex = 3;
			this.lblBuildingProductionType.Text = "0";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(3, 18);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(117, 13);
			this.label13.TabIndex = 2;
			this.label13.Text = "Время производства:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(3, 3);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(85, 13);
			this.label14.TabIndex = 2;
			this.label14.Text = "Тип продукции:";
			// 
			// tabBuildingUpgrade
			// 
			this.tabBuildingUpgrade.AutoScroll = true;
			this.tabBuildingUpgrade.Controls.Add(this.lblBuildingUpgradeMinLevel);
			this.tabBuildingUpgrade.Controls.Add(this.label16);
			this.tabBuildingUpgrade.Location = new System.Drawing.Point(4, 22);
			this.tabBuildingUpgrade.Name = "tabBuildingUpgrade";
			this.tabBuildingUpgrade.Padding = new System.Windows.Forms.Padding(3);
			this.tabBuildingUpgrade.Size = new System.Drawing.Size(315, 212);
			this.tabBuildingUpgrade.TabIndex = 4;
			this.tabBuildingUpgrade.Text = "Апгрейд";
			this.tabBuildingUpgrade.UseVisualStyleBackColor = true;
			// 
			// lblBuildingUpgradeMinLevel
			// 
			this.lblBuildingUpgradeMinLevel.AutoSize = true;
			this.lblBuildingUpgradeMinLevel.Location = new System.Drawing.Point(199, 3);
			this.lblBuildingUpgradeMinLevel.Name = "lblBuildingUpgradeMinLevel";
			this.lblBuildingUpgradeMinLevel.Size = new System.Drawing.Size(13, 13);
			this.lblBuildingUpgradeMinLevel.TabIndex = 5;
			this.lblBuildingUpgradeMinLevel.Text = "0";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(3, 3);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(198, 13);
			this.label16.TabIndex = 4;
			this.label16.Text = "Минимальный уровень для апгрейда:";
			// 
			// pictBuilding
			// 
			this.pictBuilding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictBuilding.ImageLocation = "";
			this.pictBuilding.Location = new System.Drawing.Point(3, 22);
			this.pictBuilding.Name = "pictBuilding";
			this.pictBuilding.Size = new System.Drawing.Size(128, 128);
			this.pictBuilding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictBuilding.TabIndex = 1;
			this.pictBuilding.TabStop = false;
			// 
			// lblBuildingSize
			// 
			this.lblBuildingSize.AutoSize = true;
			this.lblBuildingSize.Location = new System.Drawing.Point(239, 22);
			this.lblBuildingSize.Name = "lblBuildingSize";
			this.lblBuildingSize.Size = new System.Drawing.Size(30, 13);
			this.lblBuildingSize.TabIndex = 1;
			this.lblBuildingSize.Text = "0 x 0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(137, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 13);
			this.label7.TabIndex = 1;
			this.label7.Text = "Размеры здания:";
			// 
			// tabStart
			// 
			this.tabStart.BackColor = System.Drawing.SystemColors.Control;
			this.tabStart.Controls.Add(this.btnBasePath);
			this.tabStart.Controls.Add(this.lblDonate);
			this.tabStart.Controls.Add(this.richTextBoxAbout);
			this.tabStart.Controls.Add(this.linkSiege);
			this.tabStart.Controls.Add(this.listBoxLanguages);
			this.tabStart.Controls.Add(this.pictureBox1);
			this.tabStart.Controls.Add(this.tbxBasePath);
			this.tabStart.Controls.Add(this.label10);
			this.tabStart.Controls.Add(this.label1);
			this.tabStart.Location = new System.Drawing.Point(4, 22);
			this.tabStart.Name = "tabStart";
			this.tabStart.Padding = new System.Windows.Forms.Padding(3);
			this.tabStart.Size = new System.Drawing.Size(746, 415);
			this.tabStart.TabIndex = 2;
			this.tabStart.Text = "Осада Онлайн";
			// 
			// btnBasePath
			// 
			this.btnBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBasePath.Location = new System.Drawing.Point(711, 387);
			this.btnBasePath.Name = "btnBasePath";
			this.btnBasePath.Size = new System.Drawing.Size(31, 23);
			this.btnBasePath.TabIndex = 1;
			this.btnBasePath.Text = "...";
			this.btnBasePath.UseVisualStyleBackColor = true;
			this.btnBasePath.Click += new System.EventHandler(this.btnBasePath_Click);
			// 
			// lblDonate
			// 
			this.lblDonate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblDonate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblDonate.ForeColor = System.Drawing.Color.Red;
			this.lblDonate.Location = new System.Drawing.Point(138, 327);
			this.lblDonate.Name = "lblDonate";
			this.lblDonate.Size = new System.Drawing.Size(604, 43);
			this.lblDonate.TabIndex = 10;
			this.lblDonate.Text = "Если вы считаете, что этот справочник вам помог, то вы можете отблагодарить её ав" +
				"тора, отсыпав бриллиантов персонажу Daedra для дальнейшего развития :)";
			this.lblDonate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblDonate.Visible = false;
			// 
			// richTextBoxAbout
			// 
			this.richTextBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxAbout.BackColor = System.Drawing.SystemColors.Control;
			this.richTextBoxAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBoxAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.richTextBoxAbout.Location = new System.Drawing.Point(140, 14);
			this.richTextBoxAbout.Name = "richTextBoxAbout";
			this.richTextBoxAbout.ReadOnly = true;
			this.richTextBoxAbout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBoxAbout.Size = new System.Drawing.Size(602, 356);
			this.richTextBoxAbout.TabIndex = 9;
			this.richTextBoxAbout.TabStop = false;
			this.richTextBoxAbout.Text = resources.GetString("richTextBoxAbout.Text");
			// 
			// linkSiege
			// 
			this.linkSiege.Location = new System.Drawing.Point(6, 125);
			this.linkSiege.Name = "linkSiege";
			this.linkSiege.Size = new System.Drawing.Size(128, 18);
			this.linkSiege.TabIndex = 8;
			this.linkSiege.TabStop = true;
			this.linkSiege.Text = "www.siegeonline.ru";
			this.linkSiege.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkSiege.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSiege_LinkClicked);
			// 
			// listBoxLanguages
			// 
			this.listBoxLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.listBoxLanguages.FormattingEnabled = true;
			this.listBoxLanguages.Location = new System.Drawing.Point(4, 327);
			this.listBoxLanguages.Name = "listBoxLanguages";
			this.listBoxLanguages.Size = new System.Drawing.Size(128, 43);
			this.listBoxLanguages.TabIndex = 2;
			this.listBoxLanguages.SelectedIndexChanged += new System.EventHandler(this.listBoxLanguages_SelectedIndexChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureBox1.Image = global::SiegeOnlineDataViewer.Properties.Resources.LogoGame;
			this.pictureBox1.Location = new System.Drawing.Point(6, 14);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 110);
			this.pictureBox1.TabIndex = 6;
			this.pictureBox1.TabStop = false;
			// 
			// tbxBasePath
			// 
			this.tbxBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbxBasePath.Location = new System.Drawing.Point(6, 389);
			this.tbxBasePath.Name = "tbxBasePath";
			this.tbxBasePath.Size = new System.Drawing.Size(699, 20);
			this.tbxBasePath.TabIndex = 0;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 311);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(78, 13);
			this.label10.TabIndex = 3;
			this.label10.Text = "Локализация:";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 373);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(203, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Путь установки игры \"Осада Онлайн\":";
			// 
			// tabsMain
			// 
			this.tabsMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabsMain.Controls.Add(this.tabStart);
			this.tabsMain.Controls.Add(this.tabBuildings);
			this.tabsMain.Controls.Add(this.tabSquads);
			this.tabsMain.Controls.Add(this.tabQuest);
			this.tabsMain.Location = new System.Drawing.Point(0, 0);
			this.tabsMain.Name = "tabsMain";
			this.tabsMain.SelectedIndex = 0;
			this.tabsMain.Size = new System.Drawing.Size(754, 441);
			this.tabsMain.TabIndex = 0;
			this.tabsMain.SelectedIndexChanged += new System.EventHandler(this.tabPages_SelectedIndexChanged);
			// 
			// tabSquads
			// 
			this.tabSquads.BackColor = System.Drawing.SystemColors.Control;
			this.tabSquads.Controls.Add(this.splitContainerSquadMain);
			this.tabSquads.Location = new System.Drawing.Point(4, 22);
			this.tabSquads.Name = "tabSquads";
			this.tabSquads.Padding = new System.Windows.Forms.Padding(3);
			this.tabSquads.Size = new System.Drawing.Size(746, 415);
			this.tabSquads.TabIndex = 4;
			this.tabSquads.Text = "Войска";
			// 
			// splitContainerSquadMain
			// 
			this.splitContainerSquadMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerSquadMain.Location = new System.Drawing.Point(3, 3);
			this.splitContainerSquadMain.Name = "splitContainerSquadMain";
			// 
			// splitContainerSquadMain.Panel1
			// 
			this.splitContainerSquadMain.Panel1.Controls.Add(this.chlstFilterSquadTypes);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.chbxFilterSquadAutoUpdate);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.btnFilterSquadApply);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.btnFilterSquadReset);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.numFilterSquadLevelMin);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.numFilterSquadLevelMax);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.txtbxFilterSquadCode);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.txtbxFilterSquadName);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.label17);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.label9);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.label18);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.label19);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.label20);
			this.splitContainerSquadMain.Panel1.Controls.Add(this.label21);
			this.splitContainerSquadMain.Panel1MinSize = 160;
			// 
			// splitContainerSquadMain.Panel2
			// 
			this.splitContainerSquadMain.Panel2.Controls.Add(this.splitContainerSquadInner);
			this.splitContainerSquadMain.Size = new System.Drawing.Size(740, 409);
			this.splitContainerSquadMain.SplitterDistance = 160;
			this.splitContainerSquadMain.TabIndex = 0;
			// 
			// chlstFilterSquadTypes
			// 
			this.chlstFilterSquadTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.chlstFilterSquadTypes.CheckOnClick = true;
			this.chlstFilterSquadTypes.FormattingEnabled = true;
			this.chlstFilterSquadTypes.Items.AddRange(new object[] {
            "NPC (мобы)",
            "Войска обычные",
            "Войска с апгрейдом",
            "Войска с БР",
            "Войска с рейтом ОЗ"});
			this.chlstFilterSquadTypes.Location = new System.Drawing.Point(5, 160);
			this.chlstFilterSquadTypes.Name = "chlstFilterSquadTypes";
			this.chlstFilterSquadTypes.Size = new System.Drawing.Size(150, 79);
			this.chlstFilterSquadTypes.TabIndex = 19;
			this.chlstFilterSquadTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlstFilterTypes_ItemCheck);
			// 
			// chbxFilterSquadAutoUpdate
			// 
			this.chbxFilterSquadAutoUpdate.AutoSize = true;
			this.chbxFilterSquadAutoUpdate.Checked = true;
			this.chbxFilterSquadAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbxFilterSquadAutoUpdate.Location = new System.Drawing.Point(5, 245);
			this.chbxFilterSquadAutoUpdate.Name = "chbxFilterSquadAutoUpdate";
			this.chbxFilterSquadAutoUpdate.Size = new System.Drawing.Size(85, 17);
			this.chbxFilterSquadAutoUpdate.TabIndex = 18;
			this.chbxFilterSquadAutoUpdate.Text = "Автоапдейт";
			this.chbxFilterSquadAutoUpdate.UseVisualStyleBackColor = true;
			this.chbxFilterSquadAutoUpdate.CheckedChanged += new System.EventHandler(this.chbxFilterAutoUpdate_CheckedChanged);
			// 
			// btnFilterSquadApply
			// 
			this.btnFilterSquadApply.Enabled = false;
			this.btnFilterSquadApply.Location = new System.Drawing.Point(5, 268);
			this.btnFilterSquadApply.Name = "btnFilterSquadApply";
			this.btnFilterSquadApply.Size = new System.Drawing.Size(75, 24);
			this.btnFilterSquadApply.TabIndex = 16;
			this.btnFilterSquadApply.Text = "Применить";
			this.btnFilterSquadApply.UseVisualStyleBackColor = true;
			this.btnFilterSquadApply.Click += new System.EventHandler(this.btnFilterApply_Click);
			// 
			// btnFilterSquadReset
			// 
			this.btnFilterSquadReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFilterSquadReset.Location = new System.Drawing.Point(80, 268);
			this.btnFilterSquadReset.Name = "btnFilterSquadReset";
			this.btnFilterSquadReset.Size = new System.Drawing.Size(75, 24);
			this.btnFilterSquadReset.TabIndex = 17;
			this.btnFilterSquadReset.Text = "Сбросить";
			this.btnFilterSquadReset.UseVisualStyleBackColor = true;
			this.btnFilterSquadReset.Click += new System.EventHandler(this.btnFilterReset_Click);
			// 
			// numFilterSquadLevelMin
			// 
			this.numFilterSquadLevelMin.Location = new System.Drawing.Point(5, 121);
			this.numFilterSquadLevelMin.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterSquadLevelMin.Name = "numFilterSquadLevelMin";
			this.numFilterSquadLevelMin.Size = new System.Drawing.Size(38, 20);
			this.numFilterSquadLevelMin.TabIndex = 13;
			this.numFilterSquadLevelMin.Tag = "LevelMin";
			this.numFilterSquadLevelMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFilterSquadLevelMin.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// numFilterSquadLevelMax
			// 
			this.numFilterSquadLevelMax.Location = new System.Drawing.Point(60, 120);
			this.numFilterSquadLevelMax.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterSquadLevelMax.Name = "numFilterSquadLevelMax";
			this.numFilterSquadLevelMax.Size = new System.Drawing.Size(38, 20);
			this.numFilterSquadLevelMax.TabIndex = 14;
			this.numFilterSquadLevelMax.Tag = "LevelMax";
			this.numFilterSquadLevelMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFilterSquadLevelMax.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterSquadLevelMax.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// txtbxFilterSquadCode
			// 
			this.txtbxFilterSquadCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFilterSquadCode.Location = new System.Drawing.Point(5, 81);
			this.txtbxFilterSquadCode.Name = "txtbxFilterSquadCode";
			this.txtbxFilterSquadCode.Size = new System.Drawing.Size(150, 20);
			this.txtbxFilterSquadCode.TabIndex = 12;
			this.txtbxFilterSquadCode.Tag = "MaskCode";
			this.txtbxFilterSquadCode.TextChanged += new System.EventHandler(this.txtbxFilter_TextChanged);
			// 
			// txtbxFilterSquadName
			// 
			this.txtbxFilterSquadName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFilterSquadName.Location = new System.Drawing.Point(5, 42);
			this.txtbxFilterSquadName.Name = "txtbxFilterSquadName";
			this.txtbxFilterSquadName.Size = new System.Drawing.Size(150, 20);
			this.txtbxFilterSquadName.TabIndex = 7;
			this.txtbxFilterSquadName.Tag = "MaskName";
			this.txtbxFilterSquadName.TextChanged += new System.EventHandler(this.txtbxFilter_TextChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(46, 123);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(10, 13);
			this.label17.TabIndex = 15;
			this.label17.Text = "-";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(2, 144);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(122, 13);
			this.label9.TabIndex = 10;
			this.label9.Text = "Маска по типу войска:";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(2, 104);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(98, 13);
			this.label18.TabIndex = 10;
			this.label18.Text = "Маска по уровню:";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(2, 65);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(84, 13);
			this.label19.TabIndex = 9;
			this.label19.Text = "Маска по коду:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(2, 26);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(78, 13);
			this.label20.TabIndex = 11;
			this.label20.Text = "Маска имени:";
			// 
			// label21
			// 
			this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label21.Location = new System.Drawing.Point(5, 7);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(150, 13);
			this.label21.TabIndex = 8;
			this.label21.Text = "Фильтр поиска";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// splitContainerSquadInner
			// 
			this.splitContainerSquadInner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerSquadInner.Location = new System.Drawing.Point(0, 0);
			this.splitContainerSquadInner.Name = "splitContainerSquadInner";
			// 
			// splitContainerSquadInner.Panel1
			// 
			this.splitContainerSquadInner.Panel1.Controls.Add(this.dataGridSquads);
			this.splitContainerSquadInner.Panel1MinSize = 160;
			// 
			// splitContainerSquadInner.Panel2
			// 
			this.splitContainerSquadInner.Panel2.Controls.Add(this.linkSquadBuilding);
			this.splitContainerSquadInner.Panel2.Controls.Add(this.txtbxSquadMainDescription);
			this.splitContainerSquadInner.Panel2.Controls.Add(this.tabsSquadsInfo);
			this.splitContainerSquadInner.Panel2.Controls.Add(this.lblSquadTitle);
			this.splitContainerSquadInner.Panel2.Controls.Add(this.pictSquad);
			this.splitContainerSquadInner.Panel2.Controls.Add(this.lblSquadCode);
			this.splitContainerSquadInner.Size = new System.Drawing.Size(576, 409);
			this.splitContainerSquadInner.SplitterDistance = 243;
			this.splitContainerSquadInner.TabIndex = 0;
			// 
			// dataGridSquads
			// 
			this.dataGridSquads.AllowUserToAddRows = false;
			this.dataGridSquads.AllowUserToDeleteRows = false;
			this.dataGridSquads.AllowUserToOrderColumns = true;
			this.dataGridSquads.AllowUserToResizeRows = false;
			this.dataGridSquads.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridSquads.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridSquads.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridSquads.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridSquads.Location = new System.Drawing.Point(0, 0);
			this.dataGridSquads.MultiSelect = false;
			this.dataGridSquads.Name = "dataGridSquads";
			this.dataGridSquads.ReadOnly = true;
			this.dataGridSquads.RowHeadersVisible = false;
			this.dataGridSquads.RowHeadersWidth = 45;
			this.dataGridSquads.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridSquads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridSquads.Size = new System.Drawing.Size(243, 409);
			this.dataGridSquads.TabIndex = 1;
			this.dataGridSquads.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGrid_RowPrePaint);
			this.dataGridSquads.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGrid_DataBindingComplete);
			this.dataGridSquads.Resize += new System.EventHandler(this.dataGrid_Resize);
			this.dataGridSquads.SelectionChanged += new System.EventHandler(this.dataGrid_SelectionChanged);
			// 
			// linkSquadBuilding
			// 
			this.linkSquadBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.linkSquadBuilding.LinkArea = new System.Windows.Forms.LinkArea(15, 10);
			this.linkSquadBuilding.Location = new System.Drawing.Point(137, 137);
			this.linkSquadBuilding.Name = "linkSquadBuilding";
			this.linkSquadBuilding.Size = new System.Drawing.Size(189, 13);
			this.linkSquadBuilding.TabIndex = 10;
			this.linkSquadBuilding.TabStop = true;
			this.linkSquadBuilding.Text = "Производится в <building>";
			this.linkSquadBuilding.UseCompatibleTextRendering = true;
			this.linkSquadBuilding.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ControlListLinkTextOnClick);
			// 
			// txtbxSquadMainDescription
			// 
			this.txtbxSquadMainDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxSquadMainDescription.BackColor = System.Drawing.SystemColors.Control;
			this.txtbxSquadMainDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtbxSquadMainDescription.Location = new System.Drawing.Point(139, 22);
			this.txtbxSquadMainDescription.Multiline = true;
			this.txtbxSquadMainDescription.Name = "txtbxSquadMainDescription";
			this.txtbxSquadMainDescription.ReadOnly = true;
			this.txtbxSquadMainDescription.Size = new System.Drawing.Size(187, 109);
			this.txtbxSquadMainDescription.TabIndex = 8;
			this.txtbxSquadMainDescription.TabStop = false;
			// 
			// tabsSquadsInfo
			// 
			this.tabsSquadsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabsSquadsInfo.Controls.Add(this.tabSquadCommon);
			this.tabsSquadsInfo.Controls.Add(this.tabSquadTTH);
			this.tabsSquadsInfo.Controls.Add(this.tabSquadUpgrade);
			this.tabsSquadsInfo.Controls.Add(this.tabSquadCalc);
			this.tabsSquadsInfo.Location = new System.Drawing.Point(3, 156);
			this.tabsSquadsInfo.Name = "tabsSquadsInfo";
			this.tabsSquadsInfo.SelectedIndex = 0;
			this.tabsSquadsInfo.Size = new System.Drawing.Size(326, 238);
			this.tabsSquadsInfo.TabIndex = 7;
			this.tabsSquadsInfo.SelectedIndexChanged += new System.EventHandler(this.tabsInfo_SelectedIndexChanged);
			// 
			// tabSquadCommon
			// 
			this.tabSquadCommon.AutoScroll = true;
			this.tabSquadCommon.Controls.Add(this.txtbxSquadBattleDesc);
			this.tabSquadCommon.Location = new System.Drawing.Point(4, 22);
			this.tabSquadCommon.Name = "tabSquadCommon";
			this.tabSquadCommon.Padding = new System.Windows.Forms.Padding(3);
			this.tabSquadCommon.Size = new System.Drawing.Size(318, 212);
			this.tabSquadCommon.TabIndex = 0;
			this.tabSquadCommon.Tag = "Common";
			this.tabSquadCommon.Text = "Общее";
			this.tabSquadCommon.UseVisualStyleBackColor = true;
			// 
			// txtbxSquadBattleDesc
			// 
			this.txtbxSquadBattleDesc.BackColor = System.Drawing.SystemColors.Control;
			this.txtbxSquadBattleDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtbxSquadBattleDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxSquadBattleDesc.Location = new System.Drawing.Point(3, 3);
			this.txtbxSquadBattleDesc.Multiline = true;
			this.txtbxSquadBattleDesc.Name = "txtbxSquadBattleDesc";
			this.txtbxSquadBattleDesc.ReadOnly = true;
			this.txtbxSquadBattleDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtbxSquadBattleDesc.Size = new System.Drawing.Size(312, 206);
			this.txtbxSquadBattleDesc.TabIndex = 0;
			this.txtbxSquadBattleDesc.TabStop = false;
			// 
			// tabSquadTTH
			// 
			this.tabSquadTTH.Controls.Add(this.txtbxSquadTTH);
			this.tabSquadTTH.Location = new System.Drawing.Point(4, 22);
			this.tabSquadTTH.Name = "tabSquadTTH";
			this.tabSquadTTH.Padding = new System.Windows.Forms.Padding(3);
			this.tabSquadTTH.Size = new System.Drawing.Size(318, 212);
			this.tabSquadTTH.TabIndex = 5;
			this.tabSquadTTH.Tag = "TTH";
			this.tabSquadTTH.Text = "ТТХ";
			this.tabSquadTTH.UseVisualStyleBackColor = true;
			// 
			// txtbxSquadTTH
			// 
			this.txtbxSquadTTH.BackColor = System.Drawing.SystemColors.Control;
			this.txtbxSquadTTH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtbxSquadTTH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxSquadTTH.Location = new System.Drawing.Point(3, 3);
			this.txtbxSquadTTH.Multiline = true;
			this.txtbxSquadTTH.Name = "txtbxSquadTTH";
			this.txtbxSquadTTH.ReadOnly = true;
			this.txtbxSquadTTH.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtbxSquadTTH.Size = new System.Drawing.Size(312, 206);
			this.txtbxSquadTTH.TabIndex = 2;
			this.txtbxSquadTTH.TabStop = false;
			// 
			// tabSquadUpgrade
			// 
			this.tabSquadUpgrade.AutoScroll = true;
			this.tabSquadUpgrade.Controls.Add(this.txtbxSquadUpgradeInfo);
			this.tabSquadUpgrade.Location = new System.Drawing.Point(4, 22);
			this.tabSquadUpgrade.Name = "tabSquadUpgrade";
			this.tabSquadUpgrade.Padding = new System.Windows.Forms.Padding(3);
			this.tabSquadUpgrade.Size = new System.Drawing.Size(318, 212);
			this.tabSquadUpgrade.TabIndex = 4;
			this.tabSquadUpgrade.Tag = "Upgrade";
			this.tabSquadUpgrade.Text = "Апгрейд";
			this.tabSquadUpgrade.UseVisualStyleBackColor = true;
			// 
			// txtbxSquadUpgradeInfo
			// 
			this.txtbxSquadUpgradeInfo.BackColor = System.Drawing.SystemColors.Control;
			this.txtbxSquadUpgradeInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtbxSquadUpgradeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxSquadUpgradeInfo.Location = new System.Drawing.Point(3, 3);
			this.txtbxSquadUpgradeInfo.Multiline = true;
			this.txtbxSquadUpgradeInfo.Name = "txtbxSquadUpgradeInfo";
			this.txtbxSquadUpgradeInfo.ReadOnly = true;
			this.txtbxSquadUpgradeInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtbxSquadUpgradeInfo.Size = new System.Drawing.Size(312, 206);
			this.txtbxSquadUpgradeInfo.TabIndex = 1;
			this.txtbxSquadUpgradeInfo.TabStop = false;
			// 
			// tabSquadCalc
			// 
			this.tabSquadCalc.Controls.Add(this.grpbxSquadCalc);
			this.tabSquadCalc.Controls.Add(this.chbxSquadCalcLastLevels);
			this.tabSquadCalc.Controls.Add(this.numSquadCalcTryDiamonds);
			this.tabSquadCalc.Controls.Add(this.numSquadCalcTryCounts);
			this.tabSquadCalc.Controls.Add(this.numSquadCalcLevel);
			this.tabSquadCalc.Controls.Add(this.label25);
			this.tabSquadCalc.Controls.Add(this.label24);
			this.tabSquadCalc.Controls.Add(this.label23);
			this.tabSquadCalc.Controls.Add(this.txtbxSquadCalc);
			this.tabSquadCalc.Location = new System.Drawing.Point(4, 22);
			this.tabSquadCalc.Name = "tabSquadCalc";
			this.tabSquadCalc.Padding = new System.Windows.Forms.Padding(3);
			this.tabSquadCalc.Size = new System.Drawing.Size(318, 212);
			this.tabSquadCalc.TabIndex = 2;
			this.tabSquadCalc.Tag = "Calc";
			this.tabSquadCalc.Text = "Стоимость";
			this.tabSquadCalc.UseVisualStyleBackColor = true;
			// 
			// grpbxSquadCalc
			// 
			this.grpbxSquadCalc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpbxSquadCalc.Controls.Add(this.label26);
			this.grpbxSquadCalc.Controls.Add(this.lblSquadCalcSell);
			this.grpbxSquadCalc.Controls.Add(this.lblSquadCalcBuy);
			this.grpbxSquadCalc.Controls.Add(this.numSquadCalcPack);
			this.grpbxSquadCalc.Location = new System.Drawing.Point(3, 103);
			this.grpbxSquadCalc.Name = "grpbxSquadCalc";
			this.grpbxSquadCalc.Size = new System.Drawing.Size(312, 59);
			this.grpbxSquadCalc.TabIndex = 6;
			this.grpbxSquadCalc.TabStop = false;
			this.grpbxSquadCalc.Text = "Расчётная стоимость: ";
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(46, 21);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(20, 13);
			this.label26.TabIndex = 5;
			this.label26.Text = "шт";
			// 
			// lblSquadCalcSell
			// 
			this.lblSquadCalcSell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSquadCalcSell.Location = new System.Drawing.Point(72, 39);
			this.lblSquadCalcSell.Name = "lblSquadCalcSell";
			this.lblSquadCalcSell.Size = new System.Drawing.Size(237, 17);
			this.lblSquadCalcSell.TabIndex = 3;
			this.lblSquadCalcSell.Text = "Продажи:";
			this.lblSquadCalcSell.Visible = false;
			// 
			// lblSquadCalcBuy
			// 
			this.lblSquadCalcBuy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSquadCalcBuy.Location = new System.Drawing.Point(72, 21);
			this.lblSquadCalcBuy.Name = "lblSquadCalcBuy";
			this.lblSquadCalcBuy.Size = new System.Drawing.Size(234, 18);
			this.lblSquadCalcBuy.TabIndex = 3;
			this.lblSquadCalcBuy.Text = "Стоимость:";
			// 
			// numSquadCalcPack
			// 
			this.numSquadCalcPack.Location = new System.Drawing.Point(6, 19);
			this.numSquadCalcPack.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.numSquadCalcPack.Name = "numSquadCalcPack";
			this.numSquadCalcPack.Size = new System.Drawing.Size(40, 20);
			this.numSquadCalcPack.TabIndex = 4;
			this.numSquadCalcPack.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSquadCalcPack.ValueChanged += new System.EventHandler(this.numSquadCalc_ValueChanged);
			// 
			// chbxSquadCalcLastLevels
			// 
			this.chbxSquadCalcLastLevels.AutoSize = true;
			this.chbxSquadCalcLastLevels.Checked = true;
			this.chbxSquadCalcLastLevels.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbxSquadCalcLastLevels.Location = new System.Drawing.Point(3, 80);
			this.chbxSquadCalcLastLevels.Name = "chbxSquadCalcLastLevels";
			this.chbxSquadCalcLastLevels.Size = new System.Drawing.Size(260, 17);
			this.chbxSquadCalcLastLevels.TabIndex = 5;
			this.chbxSquadCalcLastLevels.Text = "Расчёт последнего уровня (или всей линейки)";
			this.chbxSquadCalcLastLevels.UseVisualStyleBackColor = true;
			this.chbxSquadCalcLastLevels.CheckedChanged += new System.EventHandler(this.numSquadCalc_ValueChanged);
			// 
			// numSquadCalcTryDiamonds
			// 
			this.numSquadCalcTryDiamonds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numSquadCalcTryDiamonds.DecimalPlaces = 2;
			this.numSquadCalcTryDiamonds.Location = new System.Drawing.Point(262, 54);
			this.numSquadCalcTryDiamonds.Name = "numSquadCalcTryDiamonds";
			this.numSquadCalcTryDiamonds.Size = new System.Drawing.Size(53, 20);
			this.numSquadCalcTryDiamonds.TabIndex = 4;
			this.numSquadCalcTryDiamonds.ValueChanged += new System.EventHandler(this.numSquadCalc_ValueChanged);
			// 
			// numSquadCalcTryCounts
			// 
			this.numSquadCalcTryCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numSquadCalcTryCounts.Location = new System.Drawing.Point(262, 29);
			this.numSquadCalcTryCounts.Name = "numSquadCalcTryCounts";
			this.numSquadCalcTryCounts.Size = new System.Drawing.Size(53, 20);
			this.numSquadCalcTryCounts.TabIndex = 4;
			this.numSquadCalcTryCounts.ValueChanged += new System.EventHandler(this.numSquadCalc_ValueChanged);
			// 
			// numSquadCalcLevel
			// 
			this.numSquadCalcLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numSquadCalcLevel.Location = new System.Drawing.Point(262, 6);
			this.numSquadCalcLevel.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numSquadCalcLevel.Name = "numSquadCalcLevel";
			this.numSquadCalcLevel.Size = new System.Drawing.Size(53, 20);
			this.numSquadCalcLevel.TabIndex = 4;
			this.numSquadCalcLevel.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numSquadCalcLevel.ValueChanged += new System.EventHandler(this.numSquadCalc_ValueChanged);
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(0, 56);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(251, 13);
			this.label25.TabIndex = 3;
			this.label25.Text = "Среднее кол-во бриллиантов уровень апгрейда:";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(0, 31);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(244, 13);
			this.label24.TabIndex = 3;
			this.label24.Text = "Среднее кол-во попыток на уровень апгрейда:";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(0, 8);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(143, 13);
			this.label23.TabIndex = 3;
			this.label23.Text = "Расчётный уровень юнита:";
			// 
			// txtbxSquadCalc
			// 
			this.txtbxSquadCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxSquadCalc.BackColor = System.Drawing.SystemColors.Control;
			this.txtbxSquadCalc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtbxSquadCalc.Location = new System.Drawing.Point(3, 168);
			this.txtbxSquadCalc.Multiline = true;
			this.txtbxSquadCalc.Name = "txtbxSquadCalc";
			this.txtbxSquadCalc.ReadOnly = true;
			this.txtbxSquadCalc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtbxSquadCalc.Size = new System.Drawing.Size(312, 38);
			this.txtbxSquadCalc.TabIndex = 2;
			this.txtbxSquadCalc.TabStop = false;
			// 
			// lblSquadTitle
			// 
			this.lblSquadTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSquadTitle.BackColor = System.Drawing.SystemColors.Control;
			this.lblSquadTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblSquadTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblSquadTitle.Location = new System.Drawing.Point(0, 0);
			this.lblSquadTitle.Name = "lblSquadTitle";
			this.lblSquadTitle.ReadOnly = true;
			this.lblSquadTitle.Size = new System.Drawing.Size(329, 16);
			this.lblSquadTitle.TabIndex = 6;
			this.lblSquadTitle.TabStop = false;
			this.lblSquadTitle.Text = "{code}";
			// 
			// pictSquad
			// 
			this.pictSquad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictSquad.ImageLocation = "";
			this.pictSquad.Location = new System.Drawing.Point(3, 22);
			this.pictSquad.Name = "pictSquad";
			this.pictSquad.Size = new System.Drawing.Size(128, 128);
			this.pictSquad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictSquad.TabIndex = 5;
			this.pictSquad.TabStop = false;
			// 
			// lblSquadCode
			// 
			this.lblSquadCode.BackColor = System.Drawing.SystemColors.Control;
			this.lblSquadCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblSquadCode.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblSquadCode.Location = new System.Drawing.Point(0, 396);
			this.lblSquadCode.Name = "lblSquadCode";
			this.lblSquadCode.ReadOnly = true;
			this.lblSquadCode.Size = new System.Drawing.Size(329, 13);
			this.lblSquadCode.TabIndex = 4;
			this.lblSquadCode.TabStop = false;
			this.lblSquadCode.Text = "{code}";
			// 
			// tabQuest
			// 
			this.tabQuest.BackColor = System.Drawing.SystemColors.Control;
			this.tabQuest.Controls.Add(this.splitContainerQuestMain);
			this.tabQuest.Location = new System.Drawing.Point(4, 22);
			this.tabQuest.Name = "tabQuest";
			this.tabQuest.Padding = new System.Windows.Forms.Padding(3);
			this.tabQuest.Size = new System.Drawing.Size(746, 415);
			this.tabQuest.TabIndex = 5;
			this.tabQuest.Text = "Квесты";
			// 
			// splitContainerQuestMain
			// 
			this.splitContainerQuestMain.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainerQuestMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerQuestMain.Location = new System.Drawing.Point(3, 3);
			this.splitContainerQuestMain.Name = "splitContainerQuestMain";
			// 
			// splitContainerQuestMain.Panel1
			// 
			this.splitContainerQuestMain.Panel1.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainerQuestMain.Panel1.Controls.Add(this.label8);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.chlstFilterQuestTypes);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.chbxFilterQuestAutoUpdate);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.btnFilterQuestApply);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.btnFilterQuestReset);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.numFilterQuestLevelMin);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.numFilterQuestLevelMax);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.txtbxFilterQuestCode);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.txtbxFilterQuestName);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.label27);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.label28);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.label29);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.label30);
			this.splitContainerQuestMain.Panel1.Controls.Add(this.label31);
			this.splitContainerQuestMain.Panel1MinSize = 160;
			// 
			// splitContainerQuestMain.Panel2
			// 
			this.splitContainerQuestMain.Panel2.BackColor = System.Drawing.Color.Transparent;
			this.splitContainerQuestMain.Panel2.Controls.Add(this.splitContainerQuestInner);
			this.splitContainerQuestMain.Size = new System.Drawing.Size(740, 409);
			this.splitContainerQuestMain.SplitterDistance = 160;
			this.splitContainerQuestMain.TabIndex = 0;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(2, 144);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(121, 13);
			this.label8.TabIndex = 32;
			this.label8.Text = "Маска по типу квеста:";
			// 
			// chlstFilterQuestTypes
			// 
			this.chlstFilterQuestTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.chlstFilterQuestTypes.CheckOnClick = true;
			this.chlstFilterQuestTypes.FormattingEnabled = true;
			this.chlstFilterQuestTypes.Items.AddRange(new object[] {
            "Ежедневные квесты"});
			this.chlstFilterQuestTypes.Location = new System.Drawing.Point(5, 160);
			this.chlstFilterQuestTypes.Name = "chlstFilterQuestTypes";
			this.chlstFilterQuestTypes.Size = new System.Drawing.Size(150, 64);
			this.chlstFilterQuestTypes.TabIndex = 31;
			this.chlstFilterQuestTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlstFilterTypes_ItemCheck);
			// 
			// chbxFilterQuestAutoUpdate
			// 
			this.chbxFilterQuestAutoUpdate.AutoSize = true;
			this.chbxFilterQuestAutoUpdate.Checked = true;
			this.chbxFilterQuestAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbxFilterQuestAutoUpdate.Location = new System.Drawing.Point(5, 230);
			this.chbxFilterQuestAutoUpdate.Name = "chbxFilterQuestAutoUpdate";
			this.chbxFilterQuestAutoUpdate.Size = new System.Drawing.Size(85, 17);
			this.chbxFilterQuestAutoUpdate.TabIndex = 30;
			this.chbxFilterQuestAutoUpdate.Text = "Автоапдейт";
			this.chbxFilterQuestAutoUpdate.UseVisualStyleBackColor = true;
			this.chbxFilterQuestAutoUpdate.CheckedChanged += new System.EventHandler(this.chbxFilterAutoUpdate_CheckedChanged);
			// 
			// btnFilterQuestApply
			// 
			this.btnFilterQuestApply.Enabled = false;
			this.btnFilterQuestApply.Location = new System.Drawing.Point(5, 253);
			this.btnFilterQuestApply.Name = "btnFilterQuestApply";
			this.btnFilterQuestApply.Size = new System.Drawing.Size(75, 24);
			this.btnFilterQuestApply.TabIndex = 28;
			this.btnFilterQuestApply.Text = "Применить";
			this.btnFilterQuestApply.UseVisualStyleBackColor = true;
			this.btnFilterQuestApply.Click += new System.EventHandler(this.btnFilterReset_Click);
			// 
			// btnFilterQuestReset
			// 
			this.btnFilterQuestReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFilterQuestReset.Location = new System.Drawing.Point(80, 253);
			this.btnFilterQuestReset.Name = "btnFilterQuestReset";
			this.btnFilterQuestReset.Size = new System.Drawing.Size(75, 24);
			this.btnFilterQuestReset.TabIndex = 29;
			this.btnFilterQuestReset.Text = "Сбросить";
			this.btnFilterQuestReset.UseVisualStyleBackColor = true;
			this.btnFilterQuestReset.Click += new System.EventHandler(this.btnFilterReset_Click);
			// 
			// numFilterQuestLevelMin
			// 
			this.numFilterQuestLevelMin.Location = new System.Drawing.Point(5, 121);
			this.numFilterQuestLevelMin.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterQuestLevelMin.Name = "numFilterQuestLevelMin";
			this.numFilterQuestLevelMin.Size = new System.Drawing.Size(38, 20);
			this.numFilterQuestLevelMin.TabIndex = 25;
			this.numFilterQuestLevelMin.Tag = "LevelMin";
			this.numFilterQuestLevelMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFilterQuestLevelMin.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// numFilterQuestLevelMax
			// 
			this.numFilterQuestLevelMax.Location = new System.Drawing.Point(60, 120);
			this.numFilterQuestLevelMax.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterQuestLevelMax.Name = "numFilterQuestLevelMax";
			this.numFilterQuestLevelMax.Size = new System.Drawing.Size(38, 20);
			this.numFilterQuestLevelMax.TabIndex = 26;
			this.numFilterQuestLevelMax.Tag = "LevelMax";
			this.numFilterQuestLevelMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFilterQuestLevelMax.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numFilterQuestLevelMax.ValueChanged += new System.EventHandler(this.numFilter_ValueChanged);
			// 
			// txtbxFilterQuestCode
			// 
			this.txtbxFilterQuestCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFilterQuestCode.Location = new System.Drawing.Point(5, 81);
			this.txtbxFilterQuestCode.Name = "txtbxFilterQuestCode";
			this.txtbxFilterQuestCode.Size = new System.Drawing.Size(150, 20);
			this.txtbxFilterQuestCode.TabIndex = 24;
			this.txtbxFilterQuestCode.Tag = "MaskCode";
			this.txtbxFilterQuestCode.TextChanged += new System.EventHandler(this.txtbxFilter_TextChanged);
			// 
			// txtbxFilterQuestName
			// 
			this.txtbxFilterQuestName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtbxFilterQuestName.Location = new System.Drawing.Point(5, 42);
			this.txtbxFilterQuestName.Name = "txtbxFilterQuestName";
			this.txtbxFilterQuestName.Size = new System.Drawing.Size(150, 20);
			this.txtbxFilterQuestName.TabIndex = 19;
			this.txtbxFilterQuestName.Tag = "MaskName";
			this.txtbxFilterQuestName.TextChanged += new System.EventHandler(this.txtbxFilter_TextChanged);
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(46, 123);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(10, 13);
			this.label27.TabIndex = 27;
			this.label27.Text = "-";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(2, 104);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(98, 13);
			this.label28.TabIndex = 22;
			this.label28.Text = "Маска по уровню:";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(2, 65);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(84, 13);
			this.label29.TabIndex = 21;
			this.label29.Text = "Маска по коду:";
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(2, 26);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(78, 13);
			this.label30.TabIndex = 23;
			this.label30.Text = "Маска имени:";
			// 
			// label31
			// 
			this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label31.Location = new System.Drawing.Point(3, 7);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(154, 13);
			this.label31.TabIndex = 20;
			this.label31.Text = "Фильтр поиска";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// splitContainerQuestInner
			// 
			this.splitContainerQuestInner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerQuestInner.Location = new System.Drawing.Point(0, 0);
			this.splitContainerQuestInner.Name = "splitContainerQuestInner";
			// 
			// splitContainerQuestInner.Panel1
			// 
			this.splitContainerQuestInner.Panel1.Controls.Add(this.dataGridQuests);
			this.splitContainerQuestInner.Panel1MinSize = 160;
			// 
			// splitContainerQuestInner.Panel2
			// 
			this.splitContainerQuestInner.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainerQuestInner.Panel2.Controls.Add(this.txtbxQuest);
			this.splitContainerQuestInner.Panel2.Controls.Add(this.lblQuestCode);
			this.splitContainerQuestInner.Size = new System.Drawing.Size(576, 409);
			this.splitContainerQuestInner.SplitterDistance = 243;
			this.splitContainerQuestInner.TabIndex = 0;
			// 
			// dataGridQuests
			// 
			this.dataGridQuests.AllowUserToAddRows = false;
			this.dataGridQuests.AllowUserToDeleteRows = false;
			this.dataGridQuests.AllowUserToOrderColumns = true;
			this.dataGridQuests.AllowUserToResizeRows = false;
			this.dataGridQuests.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridQuests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridQuests.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridQuests.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridQuests.Location = new System.Drawing.Point(0, 0);
			this.dataGridQuests.MultiSelect = false;
			this.dataGridQuests.Name = "dataGridQuests";
			this.dataGridQuests.ReadOnly = true;
			this.dataGridQuests.RowHeadersVisible = false;
			this.dataGridQuests.RowHeadersWidth = 45;
			this.dataGridQuests.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridQuests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridQuests.Size = new System.Drawing.Size(243, 409);
			this.dataGridQuests.TabIndex = 2;
			this.dataGridQuests.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGrid_RowPrePaint);
			this.dataGridQuests.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGrid_DataBindingComplete);
			this.dataGridQuests.Resize += new System.EventHandler(this.dataGrid_Resize);
			this.dataGridQuests.SelectionChanged += new System.EventHandler(this.dataGrid_SelectionChanged);
			// 
			// txtbxQuest
			// 
			this.txtbxQuest.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtbxQuest.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxQuest.Location = new System.Drawing.Point(0, 0);
			this.txtbxQuest.Name = "txtbxQuest";
			this.txtbxQuest.ReadOnly = true;
			this.txtbxQuest.Size = new System.Drawing.Size(329, 396);
			this.txtbxQuest.TabIndex = 7;
			this.txtbxQuest.Text = "";
			this.txtbxQuest.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtbxQuest_LinkClicked);
			// 
			// lblQuestCode
			// 
			this.lblQuestCode.BackColor = System.Drawing.SystemColors.Control;
			this.lblQuestCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lblQuestCode.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblQuestCode.Location = new System.Drawing.Point(0, 396);
			this.lblQuestCode.Name = "lblQuestCode";
			this.lblQuestCode.ReadOnly = true;
			this.lblQuestCode.Size = new System.Drawing.Size(329, 13);
			this.lblQuestCode.TabIndex = 6;
			this.lblQuestCode.TabStop = false;
			this.lblQuestCode.Text = "{code}";
			// 
			// chbxIconSize
			// 
			this.chbxIconSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chbxIconSize.AutoSize = true;
			this.chbxIconSize.Checked = true;
			this.chbxIconSize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chbxIconSize.Location = new System.Drawing.Point(628, 447);
			this.chbxIconSize.Name = "chbxIconSize";
			this.chbxIconSize.Size = new System.Drawing.Size(110, 17);
			this.chbxIconSize.TabIndex = 6;
			this.chbxIconSize.Text = "Большие иконки";
			this.toolTip.SetToolTip(this.chbxIconSize, "Размер иконок в списках ресурсов");
			this.chbxIconSize.UseVisualStyleBackColor = true;
			this.chbxIconSize.CheckedChanged += new System.EventHandler(this.chbxIconSize_CheckedChanged);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(754, 466);
			this.Controls.Add(this.chbxIconSize);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.tabsMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(750, 500);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Осада Онлайн: электронный справочник";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.tabBuildings.ResumeLayout(false);
			this.splitContainerBuildingsMain.Panel1.ResumeLayout(false);
			this.splitContainerBuildingsMain.Panel1.PerformLayout();
			this.splitContainerBuildingsMain.Panel2.ResumeLayout(false);
			this.splitContainerBuildingsMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingSizeB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingLevelMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingLevelMax)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterBuildingSizeA)).EndInit();
			this.splitContainerBuildingsInner.Panel1.ResumeLayout(false);
			this.splitContainerBuildingsInner.Panel2.ResumeLayout(false);
			this.splitContainerBuildingsInner.Panel2.PerformLayout();
			this.splitContainerBuildingsInner.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridBuildings)).EndInit();
			this.tabsBuildingInfo.ResumeLayout(false);
			this.tabBuildingConstruction.ResumeLayout(false);
			this.tabBuildingConstruction.PerformLayout();
			this.tabBuildingProducts.ResumeLayout(false);
			this.tabBuildingProducts.PerformLayout();
			this.tabBuildingUpgrade.ResumeLayout(false);
			this.tabBuildingUpgrade.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictBuilding)).EndInit();
			this.tabStart.ResumeLayout(false);
			this.tabStart.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabsMain.ResumeLayout(false);
			this.tabSquads.ResumeLayout(false);
			this.splitContainerSquadMain.Panel1.ResumeLayout(false);
			this.splitContainerSquadMain.Panel1.PerformLayout();
			this.splitContainerSquadMain.Panel2.ResumeLayout(false);
			this.splitContainerSquadMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numFilterSquadLevelMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterSquadLevelMax)).EndInit();
			this.splitContainerSquadInner.Panel1.ResumeLayout(false);
			this.splitContainerSquadInner.Panel2.ResumeLayout(false);
			this.splitContainerSquadInner.Panel2.PerformLayout();
			this.splitContainerSquadInner.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridSquads)).EndInit();
			this.tabsSquadsInfo.ResumeLayout(false);
			this.tabSquadCommon.ResumeLayout(false);
			this.tabSquadCommon.PerformLayout();
			this.tabSquadTTH.ResumeLayout(false);
			this.tabSquadTTH.PerformLayout();
			this.tabSquadUpgrade.ResumeLayout(false);
			this.tabSquadUpgrade.PerformLayout();
			this.tabSquadCalc.ResumeLayout(false);
			this.tabSquadCalc.PerformLayout();
			this.grpbxSquadCalc.ResumeLayout(false);
			this.grpbxSquadCalc.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcPack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcTryDiamonds)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcTryCounts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSquadCalcLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictSquad)).EndInit();
			this.tabQuest.ResumeLayout(false);
			this.splitContainerQuestMain.Panel1.ResumeLayout(false);
			this.splitContainerQuestMain.Panel1.PerformLayout();
			this.splitContainerQuestMain.Panel2.ResumeLayout(false);
			this.splitContainerQuestMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numFilterQuestLevelMin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFilterQuestLevelMax)).EndInit();
			this.splitContainerQuestInner.Panel1.ResumeLayout(false);
			this.splitContainerQuestInner.Panel2.ResumeLayout(false);
			this.splitContainerQuestInner.Panel2.PerformLayout();
			this.splitContainerQuestInner.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridQuests)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusBar;
		protected System.Windows.Forms.ToolStripProgressBar statProgressBar;
		protected System.Windows.Forms.ToolStripStatusLabel statText;
		private System.Windows.Forms.TabPage tabBuildings;
		private System.Windows.Forms.SplitContainer splitContainerBuildingsMain;
		private System.Windows.Forms.CheckBox chbxFilterBuildingAutoUpdate;
		private System.Windows.Forms.Button btnFilterBuildingApply;
		private System.Windows.Forms.Button btnFilterBuildingReset;
		private System.Windows.Forms.ComboBox cmbbxFilterBuildingSizeMode;
		private System.Windows.Forms.NumericUpDown numFilterBuildingSizeB;
		private System.Windows.Forms.NumericUpDown numFilterBuildingLevelMin;
		private System.Windows.Forms.NumericUpDown numFilterBuildingLevelMax;
		private System.Windows.Forms.NumericUpDown numFilterBuildingSizeA;
		private System.Windows.Forms.TextBox txtbxFilterBuildingCode;
		private System.Windows.Forms.TextBox txtbxFilterBuildingName;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.SplitContainer splitContainerBuildingsInner;
		private System.Windows.Forms.DataGridView dataGridBuildings;
		private System.Windows.Forms.TextBox lblBuildingTitle;
		private System.Windows.Forms.TextBox lblBuildingCode;
		private System.Windows.Forms.TabControl tabsBuildingInfo;
		private System.Windows.Forms.TabPage tabBuildingRequirements;
		private System.Windows.Forms.TabPage tabBuildingAfterBuild;
		private System.Windows.Forms.TabPage tabBuildingConstruction;
		private System.Windows.Forms.Label lblBuildingConstTime;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabPage tabBuildingProducts;
		private System.Windows.Forms.Label lblBuildingProductionTime;
		private System.Windows.Forms.Label lblBuildingProductionType;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TabPage tabBuildingUpgrade;
		private System.Windows.Forms.Label lblBuildingUpgradeMinLevel;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.PictureBox pictBuilding;
		private System.Windows.Forms.Label lblBuildingSize;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabStart;
		private System.Windows.Forms.LinkLabel linkSiege;
		private System.Windows.Forms.ListBox listBoxLanguages;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnBasePath;
		private System.Windows.Forms.TextBox tbxBasePath;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabsMain;
		private System.Windows.Forms.TabPage tabSquads;
		private System.Windows.Forms.SplitContainer splitContainerSquadMain;
		private System.Windows.Forms.CheckBox chbxFilterSquadAutoUpdate;
		private System.Windows.Forms.Button btnFilterSquadApply;
		private System.Windows.Forms.Button btnFilterSquadReset;
		private System.Windows.Forms.NumericUpDown numFilterSquadLevelMin;
		private System.Windows.Forms.NumericUpDown numFilterSquadLevelMax;
		private System.Windows.Forms.TextBox txtbxFilterSquadCode;
		private System.Windows.Forms.TextBox txtbxFilterSquadName;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.SplitContainer splitContainerSquadInner;
		private System.Windows.Forms.DataGridView dataGridSquads;
		private System.Windows.Forms.TextBox lblSquadTitle;
		private System.Windows.Forms.PictureBox pictSquad;
		private System.Windows.Forms.TextBox lblSquadCode;
		private System.Windows.Forms.TextBox txtbxSquadMainDescription;
		private System.Windows.Forms.TabControl tabsSquadsInfo;
		private System.Windows.Forms.TabPage tabSquadCommon;
		private System.Windows.Forms.TextBox txtbxSquadBattleDesc;
		private System.Windows.Forms.TabPage tabSquadUpgrade;
		private System.Windows.Forms.TabPage tabSquadCalc;
		private System.Windows.Forms.CheckBox chbxIconSize;
		private System.Windows.Forms.LinkLabel linkSquadBuilding;
		private System.Windows.Forms.TextBox txtbxSquadUpgradeInfo;
		private System.Windows.Forms.RichTextBox richTextBoxAbout;
		private System.Windows.Forms.TabPage tabSquadTTH;
		private System.Windows.Forms.TextBox txtbxSquadTTH;
		private System.Windows.Forms.CheckedListBox chlstFilterSquadTypes;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtbxSquadCalc;
		private System.Windows.Forms.NumericUpDown numSquadCalcLevel;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.CheckBox chbxSquadCalcLastLevels;
		private System.Windows.Forms.NumericUpDown numSquadCalcTryDiamonds;
		private System.Windows.Forms.NumericUpDown numSquadCalcTryCounts;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.GroupBox grpbxSquadCalc;
		private System.Windows.Forms.Label lblSquadCalcBuy;
		private System.Windows.Forms.Label lblSquadCalcSell;
		private System.Windows.Forms.Label lblDonate;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.NumericUpDown numSquadCalcPack;
		private System.Windows.Forms.TabPage tabQuest;
		private System.Windows.Forms.SplitContainer splitContainerQuestMain;
		private System.Windows.Forms.CheckBox chbxFilterQuestAutoUpdate;
		private System.Windows.Forms.Button btnFilterQuestApply;
		private System.Windows.Forms.Button btnFilterQuestReset;
		private System.Windows.Forms.NumericUpDown numFilterQuestLevelMin;
		private System.Windows.Forms.NumericUpDown numFilterQuestLevelMax;
		private System.Windows.Forms.TextBox txtbxFilterQuestCode;
		private System.Windows.Forms.TextBox txtbxFilterQuestName;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.SplitContainer splitContainerQuestInner;
		private System.Windows.Forms.DataGridView dataGridQuests;
		private System.Windows.Forms.ToolTip toolTip;
		private RichTextBoxEx txtbxQuest;
		private TextBox lblQuestCode;
		private Label label8;
		private CheckedListBox chlstFilterQuestTypes;
	}
}

