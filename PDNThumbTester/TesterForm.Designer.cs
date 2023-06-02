/*
 * MIT License
 * 
 * Copyright (c) 2023 Yonder
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace PDNThumbTester
{
    partial class TesterForm
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
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.loadWidthLayout = new System.Windows.Forms.TableLayoutPanel();
            this.loadWidthValueLabel = new System.Windows.Forms.Label();
            this.loadWidthLabel = new System.Windows.Forms.Label();
            this.loadWidthInput = new System.Windows.Forms.TrackBar();
            this.loadButton = new System.Windows.Forms.Button();
            this.loadStatus = new System.Windows.Forms.Label();
            this.previewContainer = new System.Windows.Forms.GroupBox();
            this.preview = new System.Windows.Forms.PictureBox();
            this.rootLayout.SuspendLayout();
            this.loadWidthLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadWidthInput)).BeginInit();
            this.previewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.loadWidthLayout, 0, 2);
            this.rootLayout.Controls.Add(this.loadButton, 0, 3);
            this.rootLayout.Controls.Add(this.loadStatus, 0, 1);
            this.rootLayout.Controls.Add(this.previewContainer, 0, 0);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(8, 8);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 4;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.rootLayout.Size = new System.Drawing.Size(784, 434);
            this.rootLayout.TabIndex = 0;
            // 
            // loadWidthLayout
            // 
            this.loadWidthLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadWidthLayout.ColumnCount = 3;
            this.loadWidthLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186F));
            this.loadWidthLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.loadWidthLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.loadWidthLayout.Controls.Add(this.loadWidthValueLabel, 1, 0);
            this.loadWidthLayout.Controls.Add(this.loadWidthLabel, 0, 0);
            this.loadWidthLayout.Controls.Add(this.loadWidthInput, 2, 0);
            this.loadWidthLayout.Location = new System.Drawing.Point(0, 372);
            this.loadWidthLayout.Margin = new System.Windows.Forms.Padding(0);
            this.loadWidthLayout.Name = "loadWidthLayout";
            this.loadWidthLayout.RowCount = 1;
            this.loadWidthLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.loadWidthLayout.Size = new System.Drawing.Size(784, 39);
            this.loadWidthLayout.TabIndex = 0;
            // 
            // loadWidthValueLabel
            // 
            this.loadWidthValueLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadWidthValueLabel.AutoSize = true;
            this.loadWidthValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadWidthValueLabel.Location = new System.Drawing.Point(195, 13);
            this.loadWidthValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this.loadWidthValueLabel.Name = "loadWidthValueLabel";
            this.loadWidthValueLabel.Size = new System.Drawing.Size(14, 13);
            this.loadWidthValueLabel.TabIndex = 0;
            this.loadWidthValueLabel.Text = "0";
            // 
            // loadWidthLabel
            // 
            this.loadWidthLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadWidthLabel.AutoSize = true;
            this.loadWidthLabel.Location = new System.Drawing.Point(4, 13);
            this.loadWidthLabel.Margin = new System.Windows.Forms.Padding(0);
            this.loadWidthLabel.Name = "loadWidthLabel";
            this.loadWidthLabel.Size = new System.Drawing.Size(178, 13);
            this.loadWidthLabel.TabIndex = 0;
            this.loadWidthLabel.Text = "Width (0 = No Resizing, Max = 256):";
            // 
            // loadWidthInput
            // 
            this.loadWidthInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.loadWidthInput.AutoSize = false;
            this.loadWidthInput.LargeChange = 1;
            this.loadWidthInput.Location = new System.Drawing.Point(219, 8);
            this.loadWidthInput.Margin = new System.Windows.Forms.Padding(0);
            this.loadWidthInput.Maximum = 256;
            this.loadWidthInput.Name = "loadWidthInput";
            this.loadWidthInput.Size = new System.Drawing.Size(565, 23);
            this.loadWidthInput.TabIndex = 1;
            this.loadWidthInput.TickFrequency = 0;
            this.loadWidthInput.TickStyle = System.Windows.Forms.TickStyle.None;
            this.loadWidthInput.ValueChanged += new System.EventHandler(this.loadWidthInput_ValueChanged);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(0, 411);
            this.loadButton.Margin = new System.Windows.Forms.Padding(0);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(784, 23);
            this.loadButton.TabIndex = 2;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // loadStatus
            // 
            this.loadStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.loadStatus.AutoSize = true;
            this.loadStatus.Location = new System.Drawing.Point(361, 359);
            this.loadStatus.Margin = new System.Windows.Forms.Padding(0);
            this.loadStatus.Name = "loadStatus";
            this.loadStatus.Size = new System.Drawing.Size(61, 13);
            this.loadStatus.TabIndex = 0;
            this.loadStatus.Text = "Waiting . . .";
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.Controls.Add(this.preview);
            this.previewContainer.Location = new System.Drawing.Point(0, 0);
            this.previewContainer.Margin = new System.Windows.Forms.Padding(0);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Padding = new System.Windows.Forms.Padding(8);
            this.previewContainer.Size = new System.Drawing.Size(784, 351);
            this.previewContainer.TabIndex = 0;
            this.previewContainer.TabStop = false;
            this.previewContainer.Text = "Preview";
            // 
            // preview
            // 
            this.preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preview.Location = new System.Drawing.Point(8, 21);
            this.preview.Margin = new System.Windows.Forms.Padding(0);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(768, 322);
            this.preview.TabIndex = 0;
            this.preview.TabStop = false;
            // 
            // TesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rootLayout);
            this.Name = "TesterForm";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Test Application for Paint.NET Project File Thumbnail Provider";
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.loadWidthLayout.ResumeLayout(false);
            this.loadWidthLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadWidthInput)).EndInit();
            this.previewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Label loadStatus;
        private System.Windows.Forms.GroupBox previewContainer;
        private System.Windows.Forms.PictureBox preview;
        private System.Windows.Forms.TableLayoutPanel loadWidthLayout;
        private System.Windows.Forms.Label loadWidthLabel;
        private System.Windows.Forms.TrackBar loadWidthInput;
        private System.Windows.Forms.Label loadWidthValueLabel;
    }
}

