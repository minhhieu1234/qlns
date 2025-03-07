﻿using HRMngt.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace HRMngt.View.Popup
{
    public partial class ThumbTicketDialog : Form, IThumbTicketDialog
    {
        private string popUpType;
        private string complainText;

        public ThumbTicketDialog(string popUpType)
        {
            InitializeComponent();
            this.popUpType = popUpType;

            if (popUpType == "Create")
            {
                this.Text = "Create Thumb/Ticket";
                lblTitle.Text = "Create Thumb/Ticket";
                btnCreateUpdate.Text = "Create";
                btnComplainResponse.Hide();
            }
            else if (popUpType == "Update")
            {
                this.Text = "Update Thumb/Ticket";
                lblTitle.Text = "Update Thumb/Ticket";
                btnCreateUpdate.Text = "Update";
                
            }    
            else if (popUpType == "Read")
            {
                this.Text = "Read Thumb/Ticket";
                lblTitle.Text = "Read Thumb/Ticket";
                btnCreateUpdate.Text = "Update";
                btnComplainResponse.Hide();
                btnCreateUpdate.Hide();
            }    

            RunEvent();
        }

        public void RunEvent()
        {
            if (btnCreateUpdate.Text == "Create")
            {
                btnCreateUpdate.Click += delegate { SaveAddEvent?.Invoke(this, EventArgs.Empty); };
            }
            else if (btnCreateUpdate.Text == "Update")
            {
                btnCreateUpdate.Click += delegate { SaveUpdateEvent?.Invoke(this, EventArgs.Empty); };
            }
            if (btnComplainResponse.Text == "Complain")
            {
                btnComplainResponse.Click += delegate { ComplainEvent?.Invoke(this, EventArgs.Empty); };
            }
            else
            {
                btnComplainResponse.Click += delegate { ResponseEvent?.Invoke(this, EventArgs.Empty); };
            }
        }

        public string Id 
        {
            get { return lblIdDetail.Text; }
            set { lblIdDetail.Text = value; }
        }


        public string Type
        {
            get {
                if (cmbTypeDetail.SelectedItem.ToString() == "")
                    return "";
                else return cmbTypeDetail.SelectedItem.ToString();  }
            set 
            {
                int index = cmbTypeDetail.FindStringExact(value);
                if (index != -1)
                    cmbTypeDetail.SelectedIndex = index;
            }
        }


        public DateTime Date
        {
            get { return dtpDateDetail.Value; }
            set { dtpDateDetail.Value = value; }
        }

        public string Sender
        {
            get
            {
                if (cmbSenderDetail.SelectedItem == null || cmbSenderDetail.SelectedItem.ToString() == "")
                    return "";
                else return ExtractIdFromName(cmbSenderDetail.SelectedItem.ToString());
            }
            set
            {
                int index = cmbSenderDetail.FindStringExact(value);
                if (index != -1)
                    cmbSenderDetail.SelectedIndex = index;
            }
        }

        public string Receiver
        {
            get
            {
                if (cmbReceiverDetail.SelectedItem == null || cmbReceiverDetail.SelectedItem.ToString() == "")
                    return "";
                else return ExtractIdFromName(cmbReceiverDetail.SelectedItem.ToString());
            }
            set
            {
                int index = cmbReceiverDetail.FindStringExact(value);
                if (index != -1) 
                    cmbReceiverDetail.SelectedIndex = index;
            }
        }

        public string Reason 
        { 
            get { return txtReasonDetail.Text; }
            set {txtReasonDetail.Text = value; }
        }

        public int Money 
        {
            get { return int.Parse(txtMoneyDetail.Text); }
            set { txtMoneyDetail.Text = value.ToString(); }
        }
        public string Complain 
        {
            get { return complainText; }
            set { lblComplainDetail.Text = value; }
        }
        public string Response 
        {
            get { return lblResponseDetail.Text; }
            set { lblResponseDetail.Text = value; }
        }
        public string Status 
        {
            get { return lblStatusDetail.Text; }
            set { lblStatusDetail.Text = value; }
        }

        // =============================================================
        // Event Handle
        public event EventHandler SaveAddEvent;
        public event EventHandler SaveUpdateEvent;
        public event EventHandler ShowUserList;
        public event EventHandler ComplainEvent;
        public event EventHandler ResponseEvent;

        // ==============================================================
        // View tasks (Presenters use these)
        public string ExtractIdFromName(string nameWithId)
        {
            if (!string.IsNullOrEmpty(nameWithId))
            {
                string[] parts = nameWithId.Split('-');
                string id = parts[0].Trim();
                return id;
            }
            else
            {
                return null;
            }
        }

        public void ShowUserIdNName(List<string> userIdNNameList)
        {
            cmbSenderDetail.Items.Clear();

            foreach (string item in userIdNNameList)
            {
                cmbSenderDetail.Items.Add(item);
                cmbReceiverDetail.Items.Add(item);
            }
        }
    }
}
