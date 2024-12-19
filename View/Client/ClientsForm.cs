using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using projet;

public class ClientsForm : Form
{
    private DataGridView dgvClients; // Tableau pour afficher les clients
    private Button btnAjouter, btnModifier, btnSupprimer;
    private ClientDAO clientDAO;
    private Panel pnlHeader, pnlFooter, pnlBody;
    private Label lblTitre;

    public ClientsForm()
    {
        clientDAO = new ClientDAO();

        // Configuration de la fenêtre
        this.Text = "Gestion des Clients";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.BackColor = Color.White;

        // En-tête
        pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 70,
            BackColor = Color.FromArgb(52, 73, 94)
        };
        lblTitre = new Label
        {
            Text = "Gestion des Clients",
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        pnlHeader.Controls.Add(lblTitre);

        // Corps principal
        pnlBody = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20)
        };

        dgvClients = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 350,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
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
                SelectionBackColor = Color.FromArgb(41, 128, 185),
                SelectionForeColor = Color.White
            }
        };

        // Pied de page avec FlowLayoutPanel pour les boutons
        pnlFooter = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 80,
            BackColor = Color.White,
            Padding = new Padding(10)
        };
        var flowLayoutPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Padding = new Padding(10),
            WrapContents = false
        };

        // Création des boutons
        Color buttonColor = Color.FromArgb(46, 204, 113); // Vert
        Color hoverColor = Color.FromArgb(39, 174, 96);  // Vert plus foncé
        btnAjouter = CreateButton("Ajouter", buttonColor, hoverColor, BtnAjouter_Click);
        btnModifier = CreateButton("Modifier", buttonColor, hoverColor, BtnModifier_Click);
        btnSupprimer = CreateButton("Supprimer", buttonColor, hoverColor, BtnSupprimer_Click);

        // Ajouter les boutons au FlowLayoutPanel
        flowLayoutPanel.Controls.Add(btnAjouter);
        flowLayoutPanel.Controls.Add(btnModifier);
        flowLayoutPanel.Controls.Add(btnSupprimer);

        // Ajouter le FlowLayoutPanel au pnlFooter
        pnlFooter.Controls.Add(flowLayoutPanel);

        // Ajout des composants
        pnlBody.Controls.Add(dgvClients);
        this.Controls.Add(pnlBody);
        this.Controls.Add(pnlFooter);
        this.Controls.Add(pnlHeader);

        // Charger les données initiales
        ChargerClients();
    }

    private Button CreateButton(string text, Color backColor, Color hoverBackColor, EventHandler clickEvent)
    {
        var button = new Button
        {
            Text = text,
            Width = 120,
            Height = 40,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
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

    private void ChargerClients()
    {
        var clients = clientDAO.RecupererTousLesClient();
        dgvClients.DataSource = clients;
    }

    private void BtnAjouter_Click(object sender, EventArgs e)
    {
        var formAjout = new ClientForm();
        if (formAjout.ShowDialog() == DialogResult.OK)
        {
            if (clientDAO.AjouterClient(formAjout.Client))
            {
                MessageBox.Show("Client ajouté avec succès !");
                ChargerClients();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout du client.");
            }
        }
    }

    private void BtnModifier_Click(object sender, EventArgs e)
    {
        if (dgvClients.CurrentRow != null)
        {
            var client = (Client)dgvClients.CurrentRow.DataBoundItem;
            var formModifier = new ClientForm(client);
            if (formModifier.ShowDialog() == DialogResult.OK)
            {
                if (clientDAO.ModifierClient(formModifier.Client))
                {
                    MessageBox.Show("Client modifié avec succès !");
                    ChargerClients();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification du client.");
                }
            }
        }
    }

    private void BtnSupprimer_Click(object sender, EventArgs e)
    {
        if (dgvClients.CurrentRow != null)
        {
            var client = (Client)dgvClients.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"Voulez-vous supprimer le client : {client.Nom} ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (clientDAO.SupprimerClient(client.Id))
                {
                    MessageBox.Show("Client supprimé avec succès !");
                    ChargerClients();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression du client.");
                }
            }
        }
    }
}
