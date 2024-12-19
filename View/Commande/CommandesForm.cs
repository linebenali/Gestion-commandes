using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class CommandesForm : Form
{
    private DataGridView dgvCommandes;
    private Button btnModifierStatut;
    private CommandeDAO commandeDAO;

    public CommandesForm()
    {
        commandeDAO = new CommandeDAO();

        this.Text = "Suivi des Commandes";
        this.Size = new System.Drawing.Size(800, 600);

        // Initialisation des composants
        dgvCommandes = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnCount = 4
        };
        dgvCommandes.Columns[0].Name = "Client";
        dgvCommandes.Columns[1].Name = "Date Commande";
        dgvCommandes.Columns[2].Name = "Statut";
        dgvCommandes.Columns[3].Name = "Détails";
        dgvCommandes.DoubleClick += DgvCommandes_DoubleClick;

        btnModifierStatut = new Button { Text = "Modifier Statut", Dock = DockStyle.Bottom };
        btnModifierStatut.Click += BtnModifierStatut_Click;

        // Ajouter les composants à la fenêtre
        this.Controls.Add(dgvCommandes);
        this.Controls.Add(btnModifierStatut);

        // Charger les commandes existantes
        ChargerCommandes();
    }

    private void ChargerCommandes()
    {
        var commandes = commandeDAO.RecupererToutesLesCommandes();
        dgvCommandes.Rows.Clear();

        foreach (var commande in commandes)
        {
            Client client = new ClientDAO().RecupererClientParId(commande.ClientId);
            dgvCommandes.Rows.Add(client.Nom, commande.DateCommande, commande.Statut, "Voir Détails");
        }
    }

    private void DgvCommandes_DoubleClick(object sender, EventArgs e)
    {
        if (dgvCommandes.CurrentRow != null)
        {
            int commandeId = (int)dgvCommandes.CurrentRow.Cells[0].Value;
            Commande commande = commandeDAO.RecupererToutesLesCommandes().FirstOrDefault(c => c.Id == commandeId);
            if (commande != null)
            {
                // Afficher les détails de la commande
                string details = $"Client: {commande.ClientId}\nDate: {commande.DateCommande}\nStatut: {commande.Statut}\n\nLignes Commande:";
                foreach (var ligne in commande.LignesCommande)
                {
                    details += $"\n- {ligne.NomArticle} (Quantité: {ligne.Quantite}, Prix Unitaire: {ligne.PrixUnitaire}, Total: {ligne.Total})";
                }
                MessageBox.Show(details, "Détails de la Commande");
            }
        }
    }

    private void BtnModifierStatut_Click(object sender, EventArgs e)
    {
        if (dgvCommandes.CurrentRow != null)
        {
            string statut = dgvCommandes.CurrentRow.Cells[2].Value.ToString();
            string nouveauStatut = statut == "En cours" ? "Livrée" : "En cours"; // Alterner le statut

            int commandeId = (int)dgvCommandes.CurrentRow.Cells[0].Value;
            Commande commande = commandeDAO.RecupererToutesLesCommandes().FirstOrDefault(c => c.Id == commandeId);
            if (commande != null)
            {
                commande.Statut = nouveauStatut;
                bool isUpdated = commandeDAO.AjouterCommande(commande); // Utiliser la méthode de mise à jour si nécessaire

                if (isUpdated)
                {
                    MessageBox.Show($"Statut de la commande mis à jour : {nouveauStatut}");
                    ChargerCommandes();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour du statut.");
                }
            }
        }
    }
}
