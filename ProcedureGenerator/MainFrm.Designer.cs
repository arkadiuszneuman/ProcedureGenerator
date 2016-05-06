namespace ProcedureGenerator
{
    partial class MainFrm
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
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.frtxtCode = new DevExpress.XtraEditors.MemoEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.frbitAlter = new DevExpress.XtraEditors.CheckEdit();
            this.btnScript = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllScripts = new DevExpress.XtraEditors.SimpleButton();
            this.uC_DatabaseSelector1 = new DatabaseSelector.View.UC_DatabaseSelector();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frbitAlter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(407, 67);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Size = new System.Drawing.Size(191, 20);
            this.lookUpEdit1.TabIndex = 2;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // frtxtCode
            // 
            this.frtxtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frtxtCode.Location = new System.Drawing.Point(22, 188);
            this.frtxtCode.Name = "frtxtCode";
            this.frtxtCode.Size = new System.Drawing.Size(891, 401);
            this.frtxtCode.TabIndex = 4;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(22, 153);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Load"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Update"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Save"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Delete")});
            this.radioGroup1.Size = new System.Drawing.Size(258, 29);
            this.radioGroup1.TabIndex = 5;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // frbitAlter
            // 
            this.frbitAlter.Location = new System.Drawing.Point(757, 40);
            this.frbitAlter.Name = "frbitAlter";
            this.frbitAlter.Properties.Caption = "Alter zamiast Create";
            this.frbitAlter.Size = new System.Drawing.Size(132, 19);
            this.frbitAlter.TabIndex = 6;
            this.frbitAlter.CheckedChanged += new System.EventHandler(this.frbitAlter_CheckedChanged);
            // 
            // btnScript
            // 
            this.btnScript.Location = new System.Drawing.Point(286, 153);
            this.btnScript.Name = "btnScript";
            this.btnScript.Size = new System.Drawing.Size(94, 23);
            this.btnScript.TabIndex = 7;
            this.btnScript.Text = "Wykonaj skrypt";
            this.btnScript.Click += new System.EventHandler(this.btnScript_Click);
            // 
            // btnAllScripts
            // 
            this.btnAllScripts.Location = new System.Drawing.Point(768, 113);
            this.btnAllScripts.Name = "btnAllScripts";
            this.btnAllScripts.Size = new System.Drawing.Size(145, 23);
            this.btnAllScripts.TabIndex = 8;
            this.btnAllScripts.Text = "Wykonaj wszystkie skrypty";
            this.btnAllScripts.Click += new System.EventHandler(this.btnAllScripts_Click);
            // 
            // uC_DatabaseSelector1
            // 
            this.uC_DatabaseSelector1.CurrentDatabaseName = "";
            this.uC_DatabaseSelector1.CurrentServerName = "";
            this.uC_DatabaseSelector1.DatabaseList = null;
            this.uC_DatabaseSelector1.IsSQLAuthentication = false;
            this.uC_DatabaseSelector1.Location = new System.Drawing.Point(12, 12);
            this.uC_DatabaseSelector1.Login = "";
            this.uC_DatabaseSelector1.Name = "uC_DatabaseSelector1";
            this.uC_DatabaseSelector1.Pass = "";
            this.uC_DatabaseSelector1.Size = new System.Drawing.Size(338, 117);
            this.uC_DatabaseSelector1.TabIndex = 9;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(504, 38);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 23);
            this.simpleButton1.TabIndex = 10;
            this.simpleButton1.Text = "Załaduj tabele";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 601);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.uC_DatabaseSelector1);
            this.Controls.Add(this.btnAllScripts);
            this.Controls.Add(this.btnScript);
            this.Controls.Add(this.frbitAlter);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.frtxtCode);
            this.Controls.Add(this.lookUpEdit1);
            this.Name = "MainFrm";
            this.Text = "MainFrm";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frtxtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frbitAlter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.MemoEdit frtxtCode;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.CheckEdit frbitAlter;
        private DevExpress.XtraEditors.SimpleButton btnScript;
        private DevExpress.XtraEditors.SimpleButton btnAllScripts;
        private DatabaseSelector.View.UC_DatabaseSelector uC_DatabaseSelector1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}