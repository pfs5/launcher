
namespace SystemTrayLauncher.Forms
{
    partial class SearchForm
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
            this.InputField = new System.Windows.Forms.TextBox();
            this.SearchResultsList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // InputField
            // 
            this.InputField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.InputField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputField.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputField.ForeColor = System.Drawing.Color.White;
            this.InputField.Location = new System.Drawing.Point(12, 12);
            this.InputField.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.InputField.Multiline = true;
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(772, 27);
            this.InputField.TabIndex = 0;
            this.InputField.TextChanged += new System.EventHandler(this.InputField_TextChanged);
            this.InputField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputField_KeyDown);
            this.InputField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputField_KeyPress);
            this.InputField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputField_KeyUp);
            // 
            // SearchResultsList
            // 
            this.SearchResultsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.SearchResultsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchResultsList.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchResultsList.ForeColor = System.Drawing.Color.White;
            this.SearchResultsList.FormattingEnabled = true;
            this.SearchResultsList.ItemHeight = 19;
            this.SearchResultsList.Location = new System.Drawing.Point(12, 47);
            this.SearchResultsList.Name = "SearchResultsList";
            this.SearchResultsList.Size = new System.Drawing.Size(772, 209);
            this.SearchResultsList.TabIndex = 1;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(798, 198);
            this.ControlBox = false;
            this.Controls.Add(this.SearchResultsList);
            this.Controls.Add(this.InputField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.Name = "SearchForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.ListBox SearchResultsList;
    }
}