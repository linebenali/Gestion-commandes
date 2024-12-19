using System;
using System.Windows.Forms;
using System.Drawing;

public class Interface1 : Form
{
    private Button btnGestionArticles;
    private Button btnGestionClients;
    private Button btnGestionCommande;

    public Interface1()
    {
        // Initializing the form
        this.Text = "Gestion de Commandes";
        this.Size = new System.Drawing.Size(400, 250); // Augmentation de la taille du formulaire pour plus de confort
        this.StartPosition = FormStartPosition.CenterScreen;

        // Initialize title label
       


        // Initialize buttons
        btnGestionArticles = new Button
        {
            Text = "Gestion des Articles",
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.LightBlue, // Couleur de fond du bouton
            Font = new Font("Arial", 12),
            ForeColor = Color.White
        };

        btnGestionClients = new Button
        {
            Text = "Gestion des Clients",
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.LightGreen,
            Font = new Font("Arial", 12),
            ForeColor = Color.White
        };

        btnGestionCommande = new Button
        {
            Text = "Gestion des Commandes",
            Dock = DockStyle.Top,
            Height = 60,
            BackColor = Color.LightCoral,
            Font = new Font("Arial", 12),
            ForeColor = Color.White
        };

        // Attach events to buttons
        btnGestionArticles.Click += BtnGestionArticles_Click;
        btnGestionClients.Click += BtnGestionClients_Click;
        btnGestionCommande.Click += BtnGestionCommande_Click;

        // Add controls to the form
        this.Controls.Add(btnGestionClients);
        this.Controls.Add(btnGestionArticles);
        this.Controls.Add(btnGestionCommande);
    }

    // Event handler for "Gestion des Articles" button
    private void BtnGestionArticles_Click(object sender, EventArgs e)
    {
        // Open the Gestion Articles form
        ArticlesForm gestionArticlesForm = new ArticlesForm();
        gestionArticlesForm.ShowDialog();
    }

    // Event handler for "Gestion des Clients" button
    private void BtnGestionClients_Click(object sender, EventArgs e)
    {
        // Open the Gestion Clients form
        ClientsForm gestionClientsForm = new ClientsForm();
        gestionClientsForm.ShowDialog();
    }

    // Event handler for "Gestion des Commandes" button
    private void BtnGestionCommande_Click(object sender, EventArgs e)
    {
        // Open the Gestion Commandes form
        GestionCommandeForm gestionCommandeForm = new GestionCommandeForm();
        gestionCommandeForm.ShowDialog();
    }
}
