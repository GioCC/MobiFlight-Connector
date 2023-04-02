namespace MobiFlight.UI.Panels.Settings.Device
{
    partial class MFEncoderPanel
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MFEncoderPanel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mfRightPinComboBox = new System.Windows.Forms.ComboBox();
            this.mfPinLabel = new System.Windows.Forms.Label();
            this.mfLeftPinComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mfEncoderTypeComboBox = new System.Windows.Forms.ComboBox();
            this.cbPin1IsMux = new System.Windows.Forms.CheckBox();
            this.cbPin2IsMux = new System.Windows.Forms.CheckBox();
            this.cbMuxPin1 = new System.Windows.Forms.ComboBox();
            this.cbMuxPin2 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMuxPin2);
            this.groupBox1.Controls.Add(this.cbMuxPin1);
            this.groupBox1.Controls.Add(this.cbPin2IsMux);
            this.groupBox1.Controls.Add(this.cbPin1IsMux);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mfRightPinComboBox);
            this.groupBox1.Controls.Add(this.mfPinLabel);
            this.groupBox1.Controls.Add(this.mfLeftPinComboBox);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // mfRightPinComboBox
            // 
            this.mfRightPinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mfRightPinComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.mfRightPinComboBox, "mfRightPinComboBox");
            this.mfRightPinComboBox.Name = "mfRightPinComboBox";
            this.mfRightPinComboBox.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // mfPinLabel
            // 
            resources.ApplyResources(this.mfPinLabel, "mfPinLabel");
            this.mfPinLabel.Name = "mfPinLabel";
            // 
            // mfLeftPinComboBox
            // 
            this.mfLeftPinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mfLeftPinComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.mfLeftPinComboBox, "mfLeftPinComboBox");
            this.mfLeftPinComboBox.Name = "mfLeftPinComboBox";
            this.mfLeftPinComboBox.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.mfEncoderTypeComboBox);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // mfEncoderTypeComboBox
            // 
            this.mfEncoderTypeComboBox.FormattingEnabled = true;
            this.mfEncoderTypeComboBox.Items.AddRange(new object[] {
            resources.GetString("mfEncoderTypeComboBox.Items"),
            resources.GetString("mfEncoderTypeComboBox.Items1"),
            resources.GetString("mfEncoderTypeComboBox.Items2"),
            resources.GetString("mfEncoderTypeComboBox.Items3"),
            resources.GetString("mfEncoderTypeComboBox.Items4")});
            resources.ApplyResources(this.mfEncoderTypeComboBox, "mfEncoderTypeComboBox");
            this.mfEncoderTypeComboBox.Name = "mfEncoderTypeComboBox";
            this.mfEncoderTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            // 
            // cbPin1IsMux
            // 
            resources.ApplyResources(this.cbPin1IsMux, "cbPin1IsMux");
            this.cbPin1IsMux.Name = "cbPin1IsMux";
            this.cbPin1IsMux.UseVisualStyleBackColor = true;
            this.cbPin1IsMux.CheckedChanged += new System.EventHandler(this.cbPin1IsMux_CheckedChanged);
            // 
            // cbPin2IsMux
            // 
            resources.ApplyResources(this.cbPin2IsMux, "cbPin2IsMux");
            this.cbPin2IsMux.Name = "cbPin2IsMux";
            this.cbPin2IsMux.UseVisualStyleBackColor = true;
            this.cbPin2IsMux.CheckedChanged += new System.EventHandler(this.cbPin2IsMux_CheckedChanged);
            // 
            // cbMuxPin1
            // 
            this.cbMuxPin1.FormattingEnabled = true;
            this.cbMuxPin1.Items.AddRange(new object[] {
            resources.GetString("cbMuxPin1.Items"),
            resources.GetString("cbMuxPin1.Items1"),
            resources.GetString("cbMuxPin1.Items2"),
            resources.GetString("cbMuxPin1.Items3"),
            resources.GetString("cbMuxPin1.Items4")});
            resources.ApplyResources(this.cbMuxPin1, "cbMuxPin1");
            this.cbMuxPin1.Name = "cbMuxPin1";
            // 
            // cbMuxPin2
            // 
            this.cbMuxPin2.FormattingEnabled = true;
            this.cbMuxPin2.Items.AddRange(new object[] {
            resources.GetString("cbMuxPin2.Items"),
            resources.GetString("cbMuxPin2.Items1"),
            resources.GetString("cbMuxPin2.Items2"),
            resources.GetString("cbMuxPin2.Items3"),
            resources.GetString("cbMuxPin2.Items4")});
            resources.ApplyResources(this.cbMuxPin2, "cbMuxPin2");
            this.cbMuxPin2.Name = "cbMuxPin2";
            // 
            // MFEncoderPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MFEncoderPanel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label mfPinLabel;
        private System.Windows.Forms.ComboBox mfLeftPinComboBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mfRightPinComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox mfEncoderTypeComboBox;
        private System.Windows.Forms.ComboBox cbMuxPin2;
        private System.Windows.Forms.ComboBox cbMuxPin1;
        private System.Windows.Forms.CheckBox cbPin2IsMux;
        private System.Windows.Forms.CheckBox cbPin1IsMux;
    }
}
