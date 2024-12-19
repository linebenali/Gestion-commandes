using System;
using System.Drawing;
using System.Windows.Forms;

public class ClientForm : Form
{
    public Client Client { get; private set; }
    private TextBox txtNom, txtAdresse, txtTelephone;
    private Button btnValider;

    public ClientForm(Client client = null)
    {
        // Configuration de la fenêtre
        this.Text = client == null ? "Ajouter un Client" : "Modifier un Client";
        this.Size = new Size(400, 350);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        // En-tête stylisé
        var pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 55,
            BackColor = Color.FromArgb(52, 73, 94)
        };
        var lblTitre = new Label
        {
            Text = client == null ? "Ajouter un Client" : "Modifier un Client",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        pnlHeader.Controls.Add(lblTitre);
        this.Controls.Add(pnlHeader);

        // Conteneur des champs
        var pnlContent = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(55)
        };

        // Champs et labels
        pnlContent.Controls.Add(CreateField("Téléphone :", out txtTelephone));
        pnlContent.Controls.Add(CreateField("Adresse :", out txtAdresse));
        pnlContent.Controls.Add(CreateField("Nom :", out txtNom));

        this.Controls.Add(pnlContent);

        // Bouton de validation
        btnValider = new Button
        {
            Text = "Valider",
            BackColor = Color.FromArgb(46, 204, 113),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Height = 40,
            Dock = DockStyle.Bottom,
            Font = new Font("Segoe UI", 12, FontStyle.Bold)
        };
        btnValider.FlatAppearance.BorderSize = 0;
        btnValider.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
        btnValider.Click += BtnValider_Click;

        this.Controls.Add(btnValider);

        // Remplir les champs en cas de modification
        if (client != null)
        {
            Client = client;
            txtNom.Text = client.Nom;
            txtAdresse.Text = client.Adresse;
            txtTelephone.Text = client.Telephone;
        }
        else
        {
            Client = new Client();
        }
    }

    // Méthode pour créer un champ avec un label
    private Panel CreateField(string labelText, out TextBox textBox)
    {
        var fieldPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 60,
            Padding = new Padding(0, 5, 0, 5)
        };

        var lbl = new Label
        {
            Text = labelText,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            ForeColor = Color.FromArgb(52, 73, 94),
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleLeft,
            Height = 20
        };

        textBox = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Top,
            Height = 30,
            Margin = new Padding(0, 5, 0, 0),
            BorderStyle = BorderStyle.FixedSingle
        };

        fieldPanel.Controls.Add(textBox);
        fieldPanel.Controls.Add(lbl);

        return fieldPanel;
    }

    private void BtnValider_Click(object sender, EventArgs e)
    {
        try
        {
            Client.Nom = txtNom.Text.Trim();
            Client.Adresse = txtAdresse.Text.Trim();
            Client.Telephone = txtTelephone.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }
        catch (Exception)
        {
            MessageBox.Show("Veuillez remplir correctement tous les champs !");
        }
    }
}
