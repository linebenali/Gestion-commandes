public class AjouterCommandeForm : Form
{
    private ComboBox cboClients, cboStatut;
    private DateTimePicker dtpDateCommande;
    private Button btnValider;
    private CommandeDAO commandeDAO;
    private ClientDAO clientDAO;

    public AjouterCommandeForm()
    {
        clientDAO = new ClientDAO();
        commandeDAO = new CommandeDAO();

        this.Text = "Ajouter une Commande";
        this.Size = new System.Drawing.Size(400, 300);

        // Initialisation des composants
        cboClients = new ComboBox { Dock = DockStyle.Top };
        cboClients.DataSource = clientDAO.RecupererTousLesClient();
        cboClients.DisplayMember = "Nom";
        cboClients.ValueMember = "Id";

        cboStatut = new ComboBox { Dock = DockStyle.Top };
        cboStatut.Items.AddRange(new string[] { "En cours", "Livrée" });
        cboStatut.SelectedIndex = 0;

        dtpDateCommande = new DateTimePicker { Dock = DockStyle.Top };

        btnValider = new Button { Text = "Valider", Dock = DockStyle.Bottom };
        btnValider.Click += BtnValider_Click;

        // Ajouter les composants
        this.Controls.Add(cboClients);
        this.Controls.Add(cboStatut);
        this.Controls.Add(dtpDateCommande);
        this.Controls.Add(btnValider);
    }

    private void BtnValider_Click(object sender, EventArgs e)
    {
        try
        {
            var commande = new Commande
            {
                ClientId = (int)cboClients.SelectedValue,
                DateCommande = dtpDateCommande.Value,
                Statut = cboStatut.SelectedItem.ToString(),
                LignesCommande = new List<LigneCommande>() // Ajouter des lignes si nécessaire
            };

            if (commandeDAO.AjouterCommande(commande))
            {
                MessageBox.Show("Commande ajoutée avec succès !");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout de la commande.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur : {ex.Message}");
        }
    }
}
