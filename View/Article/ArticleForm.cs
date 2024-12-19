using System;
using System.Drawing;
using System.Windows.Forms;

public class ArticleForm : Form
{
    public Article Article { get; private set; }
    private TextBox txtNom, txtDescription, txtCategorie, txtPrix;
    private Button btnValider;
    private Label lblNom, lblDescription, lblCategorie, lblPrix;

    public ArticleForm(Article article = null)
    {
        this.Text = article == null ? "Ajouter un Article" : "Modifier un Article";
        this.Size = new Size(450, 450);
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
            Text = article == null ? "Ajouter un Article" : "Modifier un Article",
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
        pnlContent.Controls.Add(CreateField("Nom :", out txtNom));
        pnlContent.Controls.Add(CreateField("Description :", out txtDescription));
        pnlContent.Controls.Add(CreateField("Catégorie :", out txtCategorie));
        pnlContent.Controls.Add(CreateField("Prix :", out txtPrix));
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

      

        // Remplir les champs si modification
        if (article != null)
        {
            Article = article;
            txtNom.Text = article.Nom;
            txtDescription.Text = article.Description;
            txtCategorie.Text = article.Categorie;
            txtPrix.Text = article.Prix.ToString("F2");
        }
        else
        {
            Article = new Article();
        }
    }

    private Label CreateLabel(string text)
    {
        return new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 10, FontStyle.Regular), // Taille réduite de la police
            ForeColor = Color.FromArgb(52, 152, 219), // Light blue
            Dock = DockStyle.Top,
            Padding = new Padding(5, 5, 0, 0)
        };
    }

    private TextBox CreateTextBox()
    {
        return new TextBox
        {
            Font = new Font("Segoe UI", 10), // Taille réduite de la police
            Dock = DockStyle.Top,
            Height = 35, // Réduit la hauteur des champs de texte
            Margin = new Padding(0, 5, 0, 10), // Réduit l'espacement entre les champs
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Padding = new Padding(5),
        };
    }

    private Panel CreateField(TextBox textBox, Label label)
    {
        var fieldPanel = new Panel
        {
            Dock = DockStyle.Top,
            Padding = new Padding(0, 5, 0, 15) // Réduit l'espacement entre le Label et le TextBox
        };

        fieldPanel.Controls.Add(textBox);
        fieldPanel.Controls.Add(label);
        return fieldPanel;
    }

    private void BtnValider_Click(object sender, EventArgs e)
    {
        try
        {
            Article.Nom = txtNom.Text;
            Article.Description = txtDescription.Text;
            Article.Categorie = txtCategorie.Text;
            Article.Prix = decimal.Parse(txtPrix.Text);
            this.DialogResult = DialogResult.OK;
        }
        catch
        {
            MessageBox.Show("Veuillez remplir correctement tous les champs !");
        }
    }



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




}
