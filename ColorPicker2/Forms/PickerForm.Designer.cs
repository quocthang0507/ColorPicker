﻿
namespace ColorPicker2
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
			this.panelColor.Size = new System.Drawing.Size(50, 33);
			this.panelColor.TabIndex = 0;
			// 
			// lblHexColor
			// 
			this.lblHexColor.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblHexColor.Location = new System.Drawing.Point(55, 0);
			this.lblHexColor.Name = "lblHexColor";
			this.lblHexColor.Size = new System.Drawing.Size(75, 33);
			this.lblHexColor.TabIndex = 1;
			this.lblHexColor.Text = "#";
			this.lblHexColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mouseMoveTimer
			// 
			this.mouseMoveTimer.Interval = 50;
			this.mouseMoveTimer.Tick += new System.EventHandler(this.mouseMoveTimer_Tick);
			// 
			// PickerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(130, 33);
			this.ControlBox = false;
			this.Controls.Add(this.lblHexColor);
			this.Controls.Add(this.panelColor);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PickerForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PickerForm_FormClosing);
			this.Load += new System.EventHandler(this.PickerForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelColor;
		private System.Windows.Forms.Label lblHexColor;
		private System.Windows.Forms.Timer mouseMoveTimer;
	}
}