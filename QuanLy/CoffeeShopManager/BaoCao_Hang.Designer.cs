
namespace CoffeeShopManager
{
    partial class BaoCao_Hang
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
            this.CRV_Hang = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CRV_Hang
            // 
            this.CRV_Hang.ActiveViewIndex = -1;
            this.CRV_Hang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRV_Hang.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRV_Hang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CRV_Hang.Location = new System.Drawing.Point(0, 0);
            this.CRV_Hang.Name = "CRV_Hang";
            this.CRV_Hang.Size = new System.Drawing.Size(800, 450);
            this.CRV_Hang.TabIndex = 0;
            // 
            // BaoCao_Hang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CRV_Hang);
            this.Name = "BaoCao_Hang";
            this.Text = "BaoCao_Hang";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BaoCao_Hang_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CRV_Hang;
    }
}