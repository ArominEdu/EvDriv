using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OrderingSystem
{
    public partial class OrderHere : Form
    {
        public OrderHere()
        {
            InitializeComponent();

            // Wire hover behavior for every item-panel inside pnlMenu (scalable).
            WireMenuItemHover();
        }

        private void WireMenuItemHover()
        {
            // Look for Panels that represent items inside pnlMenu.
            foreach (Control ctl in pnlMenu.Controls)
            {
                if (ctl is Panel itemPanel)
                {
                    // Find + and - buttons and a numeric Label inside this panel by convention.
                    var addBtn = itemPanel.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "+");
                    var minusBtn = itemPanel.Controls.OfType<Button>().FirstOrDefault(b => b.Text == "-");
                    var counterLabel = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => int.TryParse(l.Text, out _));
                    // Find the item label (by naming convention)
                    var itemLabel = itemPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Name != null && l.Name.StartsWith("lblItem"));

                    if (addBtn == null && minusBtn == null)
                        continue; // not an item that needs hover behavior

                    // Ensure initial state matches Designer
                    if (addBtn != null) { addBtn.Visible = false; addBtn.Enabled = false; }
                    if (minusBtn != null) { minusBtn.Visible = false; minusBtn.Enabled = false; }
                    if (counterLabel != null) counterLabel.Visible = false;
                    if (itemLabel != null) itemLabel.BackColor = SystemColors.ButtonHighlight;
                    if (itemPanel != null) itemPanel.BackColor = SystemColors.ButtonHighlight;

                    // A timer to detect when the cursor has left the panel (avoids child-controls causing false MouseLeave)
                    var leaveTimer = new System.Windows.Forms.Timer { Interval = 120 };
                    leaveTimer.Tick += (s, e) =>
                    {
                        if (!IsCursorInside(itemPanel))
                        {
                            SetHoverState(itemPanel, false, addBtn, minusBtn, counterLabel, itemLabel);
                            leaveTimer.Stop();
                        }
                    };

                    // When mouse moves anywhere over the panel or its children, show buttons and restart leave timer
                    void OnAnyMouseMove(object s, MouseEventArgs e)
                    {
                        SetHoverState(itemPanel, true, addBtn, minusBtn, counterLabel, itemLabel);
                        // restart timer to detect when the mouse leaves completely
                        leaveTimer.Stop();
                        leaveTimer.Start();
                    }

                    itemPanel.MouseMove += OnAnyMouseMove;
                    foreach (Control child in itemPanel.Controls)
                        child.MouseMove += OnAnyMouseMove;

                    // Clean up timer when panel is disposed
                    itemPanel.Disposed += (s, e) => leaveTimer.Dispose();
                }
            }
        }

        // Helper: check if mouse is still inside the given control (uses screen cursor position)
        private bool IsCursorInside(Control ctl)
        {
            var clientPoint = ctl.PointToClient(Cursor.Position);
            return ctl.ClientRectangle.Contains(clientPoint);
        }

        // Show/hide and enable/disable the add/minus controls and update appearance
        private void SetHoverState(Panel itemPanel, bool hover, Control? addBtn, Control? minusBtn, Label? counterLabel, Label? itemLabel)
        {
            if (hover)
            {
                if (addBtn != null) { addBtn.Visible = true; addBtn.Enabled = true; addBtn.BringToFront(); }
                if (minusBtn != null) { minusBtn.Visible = true; minusBtn.Enabled = true; minusBtn.BringToFront(); }
                if (counterLabel != null) counterLabel.Visible = true;
                itemPanel.BackColor = Color.Pink;
                if (itemLabel != null) itemLabel.BackColor = Color.Pink;
            }
            else
            {
                if (addBtn != null) { addBtn.Visible = false; addBtn.Enabled = false; }
                if (minusBtn != null) { minusBtn.Visible = false; minusBtn.Enabled = false; }
                if (counterLabel != null) counterLabel.Visible = false;
                itemPanel.BackColor = SystemColors.ButtonHighlight;
                if (itemLabel != null) itemLabel.BackColor = SystemColors.ButtonHighlight;
            }
        }

        private void ScrollToTop(Panel pnlmenu, Control TargetControl)
        {
            int y = TargetControl.Location.Y;
            pnlmenu.VerticalScroll.Value = y;
            pnlmenu.PerformLayout();
        }
        private void btnPopular_Click(object sender, EventArgs e)
        {
            //ScrollToTop(pnlMenu, grpPop);
        }

        private void btnBeef_Click(object sender, EventArgs e)
        {
            //ScrollToTop(pnlMenu, grpBeef);
        }

        private void btnPork_Click(object sender, EventArgs e)
        {
            // ScrollToTop(pnlMenu, grpPork);
        }

        private void btnSpecials_Click(object sender, EventArgs e)
        {
            // ScrollToTop(pnlMenu, grpSpecials);
        }

        private void btnUlam_Click(object sender, EventArgs e)
        {
            //ScrollToTop(pnlMenu, grpUlam);
        }

        private void btnMeriendas_Click(object sender, EventArgs e)
        {
            //ScrollToTop(pnlMenu, grpMerienda);
        }

        private void btnAddOn_Click(object sender, EventArgs e)
        {
            //ScrollToTop(pnlMenu, grpAddOn);
        }

        private void btnBvrg_Click(object sender, EventArgs e)
        {
            //ScrollToTop(pnlMenu, grpBvrg);
        }

        private void rndAddPop1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Added to Order");
        }

        private void rndMinusPop1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Removed from Order");
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
         
        }
    }
}
