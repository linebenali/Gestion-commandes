using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class GestionCommandeForm : Form
{
    private DataGridView dgvCommandes;
    private Button btnAjouter, btnModifier, btnSupprimer;
    private CommandeDAO commandeDAO;

    public GestionCommandeForm()
    {
        commandeDAO = new CommandeDAO();

        // Configuration principale du formulaire
        this.Text = "Gestion des Commandes";
        this.Size = new Size(900, 650);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        // Initialisation des composants
        InitialiserComposants();

        // Chargement des données initiales
        ChargerCommandes();
    }

    private void InitialiserComposants()
    {
        // En-tête avec le titre
        var pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 70,
            BackColor = Color.FromArgb(52, 152, 219),
            Padding = new Padding(10)
        };

        var lblTitre = new Label
        {
            Text = "Gestion des Commandes",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };

        pnlHeader.Controls.Add(lblTitre);

        // Corps principal avec le DataGridView
        var pnlBody = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        dgvCommandes = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = false,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            },
            DefaultCellStyle = new DataGridViewCellStyle
            {
                SelectionBackColor = Color.CornflowerBlue,
                SelectionForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            }
        };

        // Ajouter une colonne ComboBox pour le statut
        var comboBoxColumn = new DataGridViewComboBoxColumn
        {
            HeaderText = "Statut",
            Name = "Statut",
            DataSource = new List<string> { "En cours", "Terminée", "Arrêtée" }, // Les statuts
            DataPropertyName = "Statut", // La propriété à lier à la colonne
            DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        };
        dgvCommandes.Columns.Add(comboBoxColumn);

        pnlBody.Controls.Add(dgvCommandes);

        // Pied de page avec les boutons
        var pnlFooter = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 80,
            BackColor = Color.White,
            Padding = new Padding(10)
        };

        var flowPanelButtons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            WrapContents = false,
            Padding = new Padding(10)
        };

        btnAjouter = CreerBouton("Ajouter", Color.SeaGreen, Color.MediumSeaGreen, BtnAjouter_Click);
        btnModifier = CreerBouton("Modifier", Color.Orange, Color.DarkOrange, BtnModifier_Click);
        btnSupprimer = CreerBouton("Supprimer", Color.Red, Color.DarkRed, BtnSupprimer_Click);

        flowPanelButtons.Controls.Add(btnAjouter);
        flowPanelButtons.Controls.Add(btnModifier);
        flowPanelButtons.Controls.Add(btnSupprimer);

        pnlFooter.Controls.Add(flowPanelButtons);

        // Ajouter les panneaux au formulaire
        this.Controls.Add(pnlBody);
        this.Controls.Add(pnlFooter);
        this.Controls.Add(pnlHeader);
    }

    private Button CreerBouton(string text, Color backColor, Color hoverBackColor, EventHandler clickEvent)
    {
        var button = new Button
        {
            Text = text,
            Width = 120,
            Height = 40,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            BackColor = backColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(5),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = hoverBackColor;
        button.Click += clickEvent;
        return button;
    }

    private void ChargerCommandes(string statut = "Tous")
    {
        var commandes = commandeDAO.RecupererToutesLesCommandes();
        dgvCommandes.DataSource = commandes;
    }

    private void BtnAjouter_Click(object sender, EventArgs e)
    {
        var formAjout = new CommandeForm();
        if (formAjout.ShowDialog() == DialogResult.OK)
        {
            ChargerCommandes();
        }
    }

    private void BtnModifier_Click(object sender, EventArgs e)
    {
        if (dgvCommandes.CurrentRow != null)
        {
            var commande = (Commande)dgvCommandes.CurrentRow.DataBoundItem;
            var nouveauStatut = dgvCommandes.CurrentRow.Cells["Statut"].Value.ToString();

            if (commandeDAO.ModifierStatutCommande(commande.Id, nouveauStatut))
            {
                MessageBox.Show("Statut de la commande modifié avec succès.");
                ChargerCommandes();
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification du statut.");
            }
        }
        else
        {
            MessageBox.Show("Veuillez sélectionner une commande à modifier.");
        }
    }

    private void BtnSupprimer_Click(object sender, EventArgs e)
    {
        if (dgvCommandes.CurrentRow != null)
        {
            var commande = (Commande)dgvCommandes.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"Voulez-vous supprimer la commande de {commande.ClientId} ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                commandeDAO.SupprimerCommande(commande.Id);
                ChargerCommandes();
            }
        }
    }
}
