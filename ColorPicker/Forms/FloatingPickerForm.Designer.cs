
namespace ColorPicker
{
	partial class FloatingPickerForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FloatingPickerForm));
			this.panelColor = new System.Windows.Forms.Panel();
			this.lblHexColor = new System.Windows.Forms.Label();
			this.mouseMoveTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// panelColor
			// 
			this.panelColor.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelColor.Location = new System.Drawing.Point(0, 0);
			this.panelColor.Name = "panelColor";
			this.panelColor.Size = new System.Drawing.Size(50, 50);
			this.panelColor.TabIndex = 0;
			// 
			// lblHexColor
			// 
			this.lblHexColor.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblHexColor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblHexColor.Location = new System.Drawing.Point(55, 0);
			this.lblHexColor.Name = "lblHexColor";
			this.lblHexColor.Size = new System.Drawing.Size(75, 50);
			this.lblHexColor.TabIndex = 1;
			this.lblHexColor.Text = "#";
			this.lblHexColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mouseMoveTimer
			// 
			this.mouseMoveTimer.Tick += new System.EventHandler(this.mouseMoveTimer_Tick);
			// 
			// FloatingPickerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(130, 50);
			this.Controls.Add(this.lblHexColor);
			this.Controls.Add(this.panelColor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FloatingPickerForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "FloatingPickerForm";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FloatingPickerForm_FormClosing);
			this.Load += new System.EventHandler(this.FloatingPickerForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelColor;
		private System.Windows.Forms.Label lblHexColor;
		private System.Windows.Forms.Timer mouseMoveTimer;
	}
}