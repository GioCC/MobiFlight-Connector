namespace MobiFlight.UI.Panels.Config
{
    partial class PinAssignPanel
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
            if (disposing && (components != null)) {
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
            this.flpBase = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpBase
            // 
            this.flpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBase.Location = new System.Drawing.Point(0, 0);
            this.flpBase.Name = "flpBase";
            this.flpBase.Size = new System.Drawing.Size(304, 155);
            this.flpBase.TabIndex = 9;
            // 
            // PinAssignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpBase);
            this.Name = "PinAssignPanel";
            this.Size = new System.Drawing.Size(304, 155);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpBase;
    }
}
