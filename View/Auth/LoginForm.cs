using System;
using System.Drawing;
using System.Windows.Forms;

public class LoginForm : Form
{

    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtNomUtilisateur;
    private TextBox txtMotDePasse;
    private Button btnConnexion;
    private UtilisateurDAO utilisateurDAO;

    public Utilisateur UtilisateurConnecte { get; private set; }

    public LoginForm()
    {
        InitializeComponent();

    }
    private void InitializeComponent()
    {
        utilisateurDAO = new UtilisateurDAO();

        // Paramétrage du formulaire
        this.Text = "Page de Connexion";
        this.Size = new System.Drawing.Size(450, 350);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = System.Drawing.Color.White;

        // Initialisation des contrôles
        lblUsername = new Label { Text = "Nom d'utilisateur", AutoSize = true, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };
        txtNomUtilisateur = new TextBox { Font = new System.Drawing.Font("Arial", 10), Width = 200 };

     
        lblPassword = new Label { Text = "Mot de passe", AutoSize = true, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };
        txtMotDePasse = new TextBox { Font = new System.Drawing.Font("Arial", 10), UseSystemPasswordChar = true, Width = 200 };

        btnConnexion = new Button { Text = "Se connecter", Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold), BackColor = System.Drawing.Color.LightBlue, Width = 150, Height = 40 };

       
         // Configuration des positions des contrôles
        lblUsername.Location = new System.Drawing.Point(30, 30);
        txtNomUtilisateur.Location = new System.Drawing.Point(180, 30);

        lblPassword.Location = new System.Drawing.Point(30, 80);
        txtMotDePasse.Location = new System.Drawing.Point(180, 80);

        btnConnexion.Location = new System.Drawing.Point(150, 200);



        // Ajouter les contrôles au formulaire
        this.Controls.Add(lblUsername);
        this.Controls.Add(txtNomUtilisateur);
        this.Controls.Add(lblPassword);
        this.Controls.Add(txtMotDePasse);
        this.Controls.Add(btnConnexion);

        // Ajouter les événements des boutons
        btnConnexion.Click +=BtnConnexion_Click;

  
    }
    private void BtnConnexion_Click(object sender, EventArgs e)
    {
        string nomUtilisateur = txtNomUtilisateur.Text;
        string motDePasse = txtMotDePasse.Text;

        // Authentifier l'utilisateur
        Utilisateur utilisateur = utilisateurDAO.Authentifier(nomUtilisateur, motDePasse);

        if (utilisateur != null)
        {
            UtilisateurConnecte = utilisateur;
            MessageBox.Show($"Bienvenue, {utilisateur.NomUtilisateur} !", "Connexion réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }
        else
        {
            MessageBox.Show("Veuillez entrer votre nom d'utilisateur et votre mot de passe.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
