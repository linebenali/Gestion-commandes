

public class MainForm1 : Form
{
    private DataGridView dgvCommandes;
    private Button btnAjouter, btnModifier, btnSupprimer;
    private ComboBox cboStatut;
    private CommandeDAO commandeDAO;

    public MainForm1()
    {
        commandeDAO = new CommandeDAO();

        this.Text = "Gestion des Commandes";
        this.Size = new System.Drawing.Size(800, 600);

        // Initialisation des composants
        dgvCommandes = new DataGridView
        {
            Dock = DockStyle.Top,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            Height = 400
        };
        dgvCommandes.Columns.Add("Client", "Client");
        dgvCommandes.Columns.Add("DateCommande", "Date Commande");
        dgvCommandes.Columns.Add("Statut", "Statut");

        btnAjouter = new Button { Text = "Ajouter Commande", Dock = DockStyle.Left, Width = 150 };
        btnModifier = new Button { Text = "Modifier Commande", Dock = DockStyle.Left, Width = 150 };
        btnSupprimer = new Button { Text = "Supprimer Commande", Dock = DockStyle.Left, Width = 150 };

        cboStatut = new ComboBox { Dock = DockStyle.Top };
        cboStatut.Items.AddRange(new string[] { "Tous", "En cours", "Livrée" });
        cboStatut.SelectedIndex = 0; // Par défaut : "Tous"
        cboStatut.SelectedIndexChanged += CboStatut_SelectedIndexChanged;

        // Événements
        btnAjouter.Click += BtnAjouter_Click;
        btnModifier.Click += BtnModifier_Click;
        btnSupprimer.Click += BtnSupprimer_Click;

        // Ajouter les composants
        this.Controls.Add(dgvCommandes);
        this.Controls.Add(cboStatut);
        this.Controls.Add(btnAjouter);
        this.Controls.Add(btnModifier);
        this.Controls.Add(btnSupprimer);

        ChargerCommandes();
    }

    private void ChargerCommandes(string statut = "Tous")
    {
        dgvCommandes.Rows.Clear();
        var commandes = commandeDAO.RecupererToutesLesCommandes();

        if (statut != "Tous")
            commandes = commandes.Where(c => c.Statut == statut).ToList();

        foreach (var commande in commandes)
        {
            var client = new ClientDAO().RecupererClientParId(commande.ClientId);
            dgvCommandes.Rows.Add(client.Nom, commande.DateCommande, commande.Statut);
        }
    }

    private void CboStatut_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChargerCommandes(cboStatut.SelectedItem.ToString());
    }

    private void BtnAjouter_Click(object sender, EventArgs e)
    {
        var formAjout = new AjouterCommandeForm();
        if (formAjout.ShowDialog() == DialogResult.OK)
        {
            ChargerCommandes();
        }
    }

    private void BtnModifier_Click(object sender, EventArgs e)
    {
        if (dgvCommandes.CurrentRow != null)
        {
            // Implémenter la modification ici
        }
    }

    private void BtnSupprimer_Click(object sender, EventArgs e)
    {
        if (dgvCommandes.CurrentRow != null)
        {
            // Implémenter la suppression ici
        }
    }
}
