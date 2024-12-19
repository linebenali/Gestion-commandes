using System;
using System.Windows.Forms;

public class InterfaceCommerciale : Form
{
    private Button btnGestionClients;
    private Button btnGestionCommandes;
    private Button btnVueCommande;

    public InterfaceCommerciale()
    {
        // Initialisation du formulaire
        this.Text = "Interface Commerciale";
        this.Size = new System.Drawing.Size(300, 250);

        // Initialisation des boutons
        btnGestionClients = new Button
        {
            Text = "Gestion des Clients",
            Dock = DockStyle.Top,
            Height = 60
        };

        btnGestionCommandes = new Button
        {
            Text = "Gestion des Commandes",
            Dock = DockStyle.Top,
            Height = 60
        };

        btnVueCommande = new Button
        {
            Text = "Vue d'Ensemble des Commandes",
            Dock = DockStyle.Top,
            Height = 60
        };

        // Attacher les événements aux boutons
        btnGestionClients.Click += BtnGestionClients_Click;
        btnGestionCommandes.Click += BtnGestionCommandes_Click;
        // btnVueCommande.Click += BtnVueCommande_Click;

        // Ajouter les boutons au formulaire
        this.Controls.Add(btnVueCommande);
        this.Controls.Add(btnGestionCommandes);
        this.Controls.Add(btnGestionClients);
    }

    // Gestion de l'événement du bouton "Gestion des Clients"
    private void BtnGestionClients_Click(object sender, EventArgs e)
    {
        // Ouvrir le formulaire de gestion des clients
        ClientsForm gestionClientsForm = new ClientsForm();
        gestionClientsForm.ShowDialog();
    }

    // Gestion de l'événement du bouton "Gestion des Commandes"
    private void BtnGestionCommandes_Click(object sender, EventArgs e)
    {
        // Ouvrir le formulaire de gestion des commandes
        CommandeForm gestionCommandesForm = new CommandeForm();
        gestionCommandesForm.ShowDialog();
    }

    // Gestion de l'événement du bouton "Vue d'Ensemble des Commandes"
    // private void BtnVueCommande_Click(object sender, EventArgs e)
    // {
    //     // Ouvrir le formulaire de vue d'ensemble des commandes
    //     VueCommandesForm vueCommandesForm = new VueCommandesForm();
    //     vueCommandesForm.ShowDialog();
    // }
}
