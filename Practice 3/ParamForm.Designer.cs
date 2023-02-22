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
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pointSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineWidthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Радиус точки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Толщина линии";
            // 
            // pointSizeUpDown
            // 
            this.pointSizeUpDown.Location = new System.Drawing.Point(74, 104);
            this.pointSizeUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pointSizeUpDown.Name = "pointSizeUpDown";
            this.pointSizeUpDown.Size = new System.Drawing.Size(137, 27);
            this.pointSizeUpDown.TabIndex = 2;
            // 
            // lineWidthUpDown
            // 
            this.lineWidthUpDown.Location = new System.Drawing.Point(74, 247);
            this.lineWidthUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lineWidthUpDown.Name = "lineWidthUpDown";
            this.lineWidthUpDown.Size = new System.Drawing.Size(137, 27);
            this.lineWidthUpDown.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(74, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Сохранение";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 476);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lineWidthUpDown);
            this.Controls.Add(this.pointSizeUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
        private Button button1;
    }
}