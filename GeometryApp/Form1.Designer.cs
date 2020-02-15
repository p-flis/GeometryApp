namespace GeometryApp
{
    partial class Form1
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
            this.picture = new System.Windows.Forms.PictureBox();
            this.debugText = new System.Windows.Forms.Label();
            this.newPolygonButton = new System.Windows.Forms.Button();
            this.movePolygonButton = new System.Windows.Forms.Button();
            this.movePointButton = new System.Windows.Forms.Button();
            this.equalRelationButton = new System.Windows.Forms.Button();
            this.parallelRelationButton = new System.Windows.Forms.Button();
            this.divideEdgeButton = new System.Windows.Forms.Button();
            this.removeVertexButton = new System.Windows.Forms.Button();
            this.removeRelationButton = new System.Windows.Forms.Button();
            this.removePolygonButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.Location = new System.Drawing.Point(194, 52);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(1050, 700);
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.Click += new System.EventHandler(this.picture_Click);
            this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picture_MouseDown);
            this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picture_MouseMove);
            // 
            // debugText
            // 
            this.debugText.AutoSize = true;
            this.debugText.Location = new System.Drawing.Point(40, 22);
            this.debugText.Name = "debugText";
            this.debugText.Size = new System.Drawing.Size(63, 13);
            this.debugText.TabIndex = 1;
            this.debugText.Text = "debugLabel";
            this.debugText.UseMnemonic = false;
            this.debugText.Visible = false;
            // 
            // newPolygonButton
            // 
            this.newPolygonButton.Location = new System.Drawing.Point(12, 50);
            this.newPolygonButton.Name = "newPolygonButton";
            this.newPolygonButton.Size = new System.Drawing.Size(152, 23);
            this.newPolygonButton.TabIndex = 2;
            this.newPolygonButton.Text = "New polygon";
            this.newPolygonButton.UseVisualStyleBackColor = true;
            this.newPolygonButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newPolygonButton_MouseDown);
            // 
            // movePolygonButton
            // 
            this.movePolygonButton.Location = new System.Drawing.Point(12, 79);
            this.movePolygonButton.Name = "movePolygonButton";
            this.movePolygonButton.Size = new System.Drawing.Size(152, 23);
            this.movePolygonButton.TabIndex = 3;
            this.movePolygonButton.Text = "Move polygon";
            this.movePolygonButton.UseVisualStyleBackColor = true;
            this.movePolygonButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.movePolygonButton_MouseDown);
            // 
            // movePointButton
            // 
            this.movePointButton.Location = new System.Drawing.Point(12, 108);
            this.movePointButton.Name = "movePointButton";
            this.movePointButton.Size = new System.Drawing.Size(152, 23);
            this.movePointButton.TabIndex = 4;
            this.movePointButton.Text = "Move element";
            this.movePointButton.UseVisualStyleBackColor = true;
            this.movePointButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.movePointButton_MouseDown);
            // 
            // equalRelationButton
            // 
            this.equalRelationButton.Location = new System.Drawing.Point(12, 137);
            this.equalRelationButton.Name = "equalRelationButton";
            this.equalRelationButton.Size = new System.Drawing.Size(152, 23);
            this.equalRelationButton.TabIndex = 5;
            this.equalRelationButton.Text = "Equal relation";
            this.equalRelationButton.UseVisualStyleBackColor = true;
            this.equalRelationButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.equalRelationButton_MouseDown);
            // 
            // parallelRelationButton
            // 
            this.parallelRelationButton.Location = new System.Drawing.Point(12, 166);
            this.parallelRelationButton.Name = "parallelRelationButton";
            this.parallelRelationButton.Size = new System.Drawing.Size(152, 23);
            this.parallelRelationButton.TabIndex = 6;
            this.parallelRelationButton.Text = "Parallel relation";
            this.parallelRelationButton.UseVisualStyleBackColor = true;
            this.parallelRelationButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.parallelRelationButton_MouseDown);
            // 
            // divideEdgeButton
            // 
            this.divideEdgeButton.Location = new System.Drawing.Point(12, 195);
            this.divideEdgeButton.Name = "divideEdgeButton";
            this.divideEdgeButton.Size = new System.Drawing.Size(152, 23);
            this.divideEdgeButton.TabIndex = 7;
            this.divideEdgeButton.Text = "Divide edge";
            this.divideEdgeButton.UseVisualStyleBackColor = true;
            this.divideEdgeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.divideEdgeButton_MouseDown);
            // 
            // removeVertexButton
            // 
            this.removeVertexButton.Location = new System.Drawing.Point(12, 224);
            this.removeVertexButton.Name = "removeVertexButton";
            this.removeVertexButton.Size = new System.Drawing.Size(152, 23);
            this.removeVertexButton.TabIndex = 8;
            this.removeVertexButton.Text = "Remove vertex";
            this.removeVertexButton.UseVisualStyleBackColor = true;
            this.removeVertexButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.removeVertexButton_MouseDown);
            // 
            // removeRelationButton
            // 
            this.removeRelationButton.Location = new System.Drawing.Point(12, 253);
            this.removeRelationButton.Name = "removeRelationButton";
            this.removeRelationButton.Size = new System.Drawing.Size(152, 23);
            this.removeRelationButton.TabIndex = 9;
            this.removeRelationButton.Text = "Remove relation";
            this.removeRelationButton.UseVisualStyleBackColor = true;
            this.removeRelationButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.removeRelationButton_MouseDown);
            // 
            // removePolygonButton
            // 
            this.removePolygonButton.Location = new System.Drawing.Point(12, 282);
            this.removePolygonButton.Name = "removePolygonButton";
            this.removePolygonButton.Size = new System.Drawing.Size(152, 23);
            this.removePolygonButton.TabIndex = 10;
            this.removePolygonButton.Text = "Remove polygon";
            this.removePolygonButton.UseVisualStyleBackColor = true;
            this.removePolygonButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.removePolygonButton_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.removePolygonButton);
            this.Controls.Add(this.removeRelationButton);
            this.Controls.Add(this.removeVertexButton);
            this.Controls.Add(this.divideEdgeButton);
            this.Controls.Add(this.parallelRelationButton);
            this.Controls.Add(this.equalRelationButton);
            this.Controls.Add(this.movePointButton);
            this.Controls.Add(this.movePolygonButton);
            this.Controls.Add(this.newPolygonButton);
            this.Controls.Add(this.debugText);
            this.Controls.Add(this.picture);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Geometry App";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label debugText;
        private System.Windows.Forms.Button newPolygonButton;
        private System.Windows.Forms.Button movePolygonButton;
        private System.Windows.Forms.Button movePointButton;
        private System.Windows.Forms.Button equalRelationButton;
        private System.Windows.Forms.Button parallelRelationButton;
        private System.Windows.Forms.Button divideEdgeButton;
        private System.Windows.Forms.Button removeVertexButton;
        private System.Windows.Forms.Button removeRelationButton;
        private System.Windows.Forms.Button removePolygonButton;
    }
}

