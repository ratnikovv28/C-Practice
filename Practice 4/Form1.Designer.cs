using System.Windows.Forms.DataVisualization.Charting;

namespace Practice_4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chooseFolderButton = new System.Windows.Forms.Button();
            this.choosenFolderLabel = new System.Windows.Forms.Label();
            this.NUpDown = new System.Windows.Forms.NumericUpDown();
            this.NLabel = new System.Windows.Forms.Label();
            this.fileProcessingButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.NUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chooseFolderButton
            // 
            this.chooseFolderButton.Location = new System.Drawing.Point(28, 22);
            this.chooseFolderButton.Name = "chooseFolderButton";
            this.chooseFolderButton.Size = new System.Drawing.Size(105, 23);
            this.chooseFolderButton.TabIndex = 0;
            this.chooseFolderButton.Text = "Выбор папки";
            this.chooseFolderButton.UseVisualStyleBackColor = true;
            // 
            // choosenFolderLabel
            // 
            this.choosenFolderLabel.AutoSize = true;
            this.choosenFolderLabel.Location = new System.Drawing.Point(28, 48);
            this.choosenFolderLabel.Name = "choosenFolderLabel";
            this.choosenFolderLabel.Size = new System.Drawing.Size(0, 15);
            this.choosenFolderLabel.TabIndex = 1;
            // 
            // NUpDown
            // 
            this.NUpDown.Location = new System.Drawing.Point(28, 72);
            this.NUpDown.Name = "NUpDown";
            this.NUpDown.Size = new System.Drawing.Size(56, 23);
            this.NUpDown.TabIndex = 4;
            this.NUpDown.ValueChanged += new System.EventHandler(this.NUpDown_ValueChanged);
            // 
            // NLabel
            // 
            this.NLabel.AutoSize = true;
            this.NLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NLabel.Location = new System.Drawing.Point(87, 75);
            this.NLabel.Name = "NLabel";
            this.NLabel.Size = new System.Drawing.Size(42, 17);
            this.NLabel.TabIndex = 5;
            this.NLabel.Text = "N = 0";
            // 
            // fileProcessingButton
            // 
            this.fileProcessingButton.Location = new System.Drawing.Point(28, 129);
            this.fileProcessingButton.Name = "fileProcessingButton";
            this.fileProcessingButton.Size = new System.Drawing.Size(172, 23);
            this.fileProcessingButton.TabIndex = 6;
            this.fileProcessingButton.Text = "Запуск обработки";
            this.fileProcessingButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(557, 341);
            this.dataGridView1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.fileProcessingButton);
            this.Controls.Add(this.NLabel);
            this.Controls.Add(this.NUpDown);
            this.Controls.Add(this.choosenFolderLabel);
            this.Controls.Add(this.chooseFolderButton);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Обработка файлов";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button chooseFolderButton;
        private Label choosenFolderLabel;
        private NumericUpDown NUpDown;
        private Label NLabel;
        private Button fileProcessingButton;
        private DataGridView dataGridView1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}