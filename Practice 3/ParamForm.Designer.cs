namespace Practice_3
{
    partial class ParamForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pointSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.lineWidthUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pointSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineWidthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Радиус точки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Толщина линии";
            // 
            // pointSizeUpDown
            // 
            this.pointSizeUpDown.Location = new System.Drawing.Point(65, 78);
            this.pointSizeUpDown.Name = "pointSizeUpDown";
            this.pointSizeUpDown.Size = new System.Drawing.Size(120, 23);
            this.pointSizeUpDown.TabIndex = 2;
            // 
            // lineWidthUpDown
            // 
            this.lineWidthUpDown.Location = new System.Drawing.Point(65, 185);
            this.lineWidthUpDown.Name = "lineWidthUpDown";
            this.lineWidthUpDown.Size = new System.Drawing.Size(120, 23);
            this.lineWidthUpDown.TabIndex = 3;
            // 
            // ParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 357);
            this.Controls.Add(this.lineWidthUpDown);
            this.Controls.Add(this.pointSizeUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParamForms";
            ((System.ComponentModel.ISupportInitialize)(this.pointSizeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineWidthUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private NumericUpDown pointSizeUpDown;
        private NumericUpDown lineWidthUpDown;
    }
}