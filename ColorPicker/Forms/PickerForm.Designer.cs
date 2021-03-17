
namespace ColorPicker
{
	partial class PickerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickerForm));
			this.mouseMoveTimer = new System.Windows.Forms.Timer(this.components);
			this.lblHexColor = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// mouseMoveTimer
			// 
			this.mouseMoveTimer.Tick += new System.EventHandler(this.mouseMoveTimer_Tick);
			// 
			// lblHexColor
			// 
			this.lblHexColor.BackColor = System.Drawing.Color.Transparent;
			this.lblHexColor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblHexColor.Location = new System.Drawing.Point(0, 0);
			this.lblHexColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblHexColor.Name = "lblHexColor";
			this.lblHexColor.Size = new System.Drawing.Size(134, 61);
			this.lblHexColor.TabIndex = 0;
			this.lblHexColor.Text = "#";
			this.lblHexColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PickerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(134, 61);
			this.Controls.Add(this.lblHexColor);
			this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PickerForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Color Picker";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PickerForm_FormClosing);
			this.Load += new System.EventHandler(this.PickerForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer mouseMoveTimer;
		private System.Windows.Forms.Label lblHexColor;
	}
}