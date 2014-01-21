using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace WS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }
/*
        public string SetTest(string test)
        {
            Console.WriteLine("fonction setTest entree");
            try
            {
                Console.WriteLine("try");
               
                string requeteInsertion = "insert into dbo.testTable (test) VALUES (@insert)";
                string chaineConnection = "Data Source=(LocalDb)\v11.0;Initial Catalog=SearchShoot;Integrated Security=True;Pooling=False";
                var sqlconnection = new SqlConnection(chaineConnection);
                Console.WriteLine("open cnt");

                var rq = new SqlCommand(requeteInsertion, sqlconnection);
                rq.Parameters.AddWithValue("@insert", test);
                sqlconnection.Open();
                int i = rq.ExecuteNonQuery();
                sqlconnection.Close();
                Console.WriteLine("close cnt");

                return "ok"+i;
            }
            catch (Exception)
            {
                return "merde";
                
            }
            
            
        }

        public string GetTest()
        {
            string[] plop = null;

            try
            {
                string requeteSeekToken = "SELECT test from dbo.testTable";

                string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";

                var sqlconnection = new SqlConnection(chaineConnection);
                sqlconnection.Open();
                var rq = new SqlCommand(requeteSeekToken, sqlconnection);

                try
                {
                    int i = 0;
                    SqlDataReader reader = rq.ExecuteReader();
                    while (reader.Read())
                    {
                        plop[i] = (string)reader[0];
                        i++;
                    }
                    sqlconnection.Close();
                    return plop[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            catch (Exception)
            {
                return "merde";
            }

            
        }
        
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }*/
        

        public string SetInfos(string nom, string prenom, string login, string ville, int id)
        {
            string requeteInfo = "update dbo.Users set nom = @nom, prenom = @prenom, login = @login, ville = @ville  Where id = @id";
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInfo, sqlconnection);
            rq.Parameters.AddWithValue("@nom", nom);
            rq.Parameters.AddWithValue("@prenom", prenom);
            rq.Parameters.AddWithValue("@login", login);
            rq.Parameters.AddWithValue("@ville", ville);
            rq.Parameters.AddWithValue("@id", id);
            try
            {
                sqlconnection.Open();
                rq.ExecuteNonQuery();
                sqlconnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return "ok";
        }

        public bool SetLastCon(int id)
        {
            string requeteInfo = "update dbo.Users set last_connect = @date Where id = @id";
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInfo, sqlconnection);
            rq.Parameters.AddWithValue("@date", DateTime.Now);
            rq.Parameters.AddWithValue("@id", id);
            try
            {
                sqlconnection.Open();
                rq.ExecuteNonQuery();
                sqlconnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return true;
        }

        public info GetInfos(int id_joueur)
        {
            string requeteSelectInfo = "select nom, prenom, login, ville From dbo.Users Where id = @id_joueur";
            //string requeteSelectInfo = "select nom From dbo.Users Where id = @id_joueur";

            info infos = new info();
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    infos.nom = reader[0].ToString();
                    infos.prenom = reader[1].ToString();
                    infos.login = reader[2].ToString();
                    infos.ville = reader[3].ToString();
                    
                }
                sqlconnection.Close();
                return infos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int GetUserId(string mail)
        {
            string requeteSelectInfo = "select id From dbo.Users Where mail = @mail";
            int id = 0;
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);
            rq.Parameters.AddWithValue("@mail", mail);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = (int) reader[0];
                    }
                    sqlconnection.Close();
                    return id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int InscriptionTwitter(string login, byte[] picture, int connexion, string token)
        {
            string requeteInsertion =
                "insert into dbo.Users (password, login, level, score, connexion, token, date_insc, last_connect, twitter)" +
                " VALUES (@password, @login, @level, @score, @connexion, @token, @mail, @date_insc, @last_connect, @nom, @prenom)";
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@password", "");
            rq.Parameters.AddWithValue("@login", login);
            rq.Parameters.AddWithValue("@level", 1);
            rq.Parameters.AddWithValue("@score", 0);
            rq.Parameters.AddWithValue("@connexion", connexion);
            rq.Parameters.AddWithValue("@token", token);
            rq.Parameters.AddWithValue("@date_insc", DateTime.Now);
            rq.Parameters.AddWithValue("@last_connect", DateTime.Now);
            rq.Parameters.AddWithValue("@twitter", login);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            return i;
        }

        public byte[] getPic(byte[] pic)
        {
            return pic;
        }

        public int GetUserIdTwitter(string twitter)
        {
            string requeteSelectInfo = "select id From dbo.Users Where twitter = @twitter";
            int id = 0;
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);
            rq.Parameters.AddWithValue("@twitter", twitter);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = (int)reader[0];
                    }
                    sqlconnection.Close();
                    return id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public user GetUserInfos(int id)
        {
            string requeteSelectInfo = "select login, token, picture From dbo.Users where id = @id";

            user infos = new user();
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);
            rq.Parameters.AddWithValue("@id", id);

            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    infos.login = reader[0].ToString();
                    infos.token = reader[1].ToString();
                }
                sqlconnection.Close();
                return infos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public personnes GetPeopleInfos()
        {
            string requeteSelectInfo = "select id, login, score, date_insc, picture From dbo.Users order by score desc";

            personnes infos = new personnes();
            infos.id = new List<int>();
            infos.login = new List<string>();
            infos.score = new List<int>();
            infos.img = new List<byte[]>();
            infos.date_insc = new List<DateTime>();

            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            byte[] img;
            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);

            sqlconnection.Open();
            try
            {
                int i = 0;
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    infos.id.Add((int) reader[0]);
                    infos.login.Add(reader[1].ToString());
                    infos.score.Add((int)reader[2]); 
                    infos.date_insc.Add((DateTime)reader[3]);
                    /*infos.id= (int)reader[0];
                    infos.login =reader[1].ToString();
                    infos.score = (int) reader[2];*/
                    //infos.date_insc.SetValue((DateTime)reader[3], i);
/*                    try
                    {

                        using (MemoryStream ms = new MemoryStream())
                        {
                            System.Drawing.Image image = (System.Drawing.Image) ima;
                            image.Save(ms, ImageFormat.Jpeg);
                            img = ms.ToArray();
                            infos.img.SetValue(img, i);
                        }
                    }
                    catch (Exception) { throw; } 
                    //infos.img.SetValue(reader[4], i);*/
                    i++;
                }
                sqlconnection.Close();
                return infos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public personnes GetFriendInfos(int id)
        {
            string requeteSelectInfo = "select dbo.Users.id, login, score, date_insc, picture, id_user, id_friend From dbo.Users, dbo.Friends where dbo.Users.id = id_friend AND id_user = @id order by score desc";

            personnes infos = new personnes();
            infos.id = new List<int>();
            infos.login = new List<string>();
            infos.score = new List<int>();
            infos.img = new List<byte[]>();
            infos.date_insc = new List<DateTime>();

            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            byte[] img;
            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);
            rq.Parameters.AddWithValue("@id", id);
            sqlconnection.Open();
            try
            {
                int i = 0;
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    infos.id.Add((int)reader[0]);
                    infos.login.Add(reader[1].ToString());
                    infos.score.Add((int)reader[2]);
                    infos.date_insc.Add((DateTime)reader[3]);
                    /*infos.id= (int)reader[0];
                    infos.login =reader[1].ToString();
                    infos.score = (int) reader[2];*/
                    //infos.date_insc.SetValue((DateTime)reader[3], i);
                    /*                    try
                                        {

                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                System.Drawing.Image image = (System.Drawing.Image) ima;
                                                image.Save(ms, ImageFormat.Jpeg);
                                                img = ms.ToArray();
                                                infos.img.SetValue(img, i);
                                            }
                                        }
                                        catch (Exception) { throw; } 
                                        //infos.img.SetValue(reader[4], i);*/
                    i++;
                }
                sqlconnection.Close();
                return infos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int GetScore(int id)
        {
            string requeteSelectInfo = "select score From dbo.Users where id = @id";

            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            var rq = new SqlCommand(requeteSelectInfo, sqlconnection);
            rq.Parameters.AddWithValue("@id", id);
            int score = 0;
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    score = (int) reader[0];
                }
                sqlconnection.Close();
                return score;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int InscriptionSocial(string login, string nom, string prenom,  string mail, byte[] picture, int connexion, string token)
        {
            //refaire la requete
            string requeteInsertion = "insert into dbo.Users (password, login, level, score, connexion, token, mail, date_insc, last_connect, nom, prenom)" +
                                       " VALUES (@password, @login, @level, @score, @connexion, @token, @mail, @date_insc, @last_connect, @nom, @prenom)";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@password", "");
            rq.Parameters.AddWithValue("@login", login);
            rq.Parameters.AddWithValue("@level", 1);
            rq.Parameters.AddWithValue("@score", 0);
            rq.Parameters.AddWithValue("@connexion", connexion);
            rq.Parameters.AddWithValue("@token", token);
            rq.Parameters.AddWithValue("@mail", mail);
            rq.Parameters.AddWithValue("@date_insc", DateTime.Now);
            rq.Parameters.AddWithValue("@last_connect", DateTime.Now);
            rq.Parameters.AddWithValue("@nom", nom);
            rq.Parameters.AddWithValue("@prenom", prenom);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            return i;
        }

        public int Inscription(string password, string login, string mail)
        {
            //refaire la requete
            string requeteInsertion =  "insert into dbo.Users (password, login, level, score, connexion, token, mail, date_insc, last_connect)" +
                                       " VALUES (@password, @login, @level, @score, @connexion, @token, @mail, @date_insc, @last_connect)";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@password", password);
            rq.Parameters.AddWithValue("@login", login);
            rq.Parameters.AddWithValue("@level", 1);
            rq.Parameters.AddWithValue("@score", 0);
            rq.Parameters.AddWithValue("@connexion", 1);
            rq.Parameters.AddWithValue("@token", "");
            rq.Parameters.AddWithValue("@mail", mail);
           rq.Parameters.AddWithValue("@date_insc", DateTime.Now);
            rq.Parameters.AddWithValue("@last_connect", DateTime.Now);


            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();

            return  i;
        }

        public int Authentification(string login, string password)
        {
            string recup = "";
            try
            {
                string requeteSeekToken = "SELECT id from dbo.Users WHERE login = @login AND password = @password";

                string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";

                var sqlconnection = new SqlConnection(chaineConnection);
                sqlconnection.Open();
                var rq = new SqlCommand(requeteSeekToken, sqlconnection);
                rq.Parameters.AddWithValue("@password", password);
                rq.Parameters.AddWithValue("@login", login);
                try
                {
                    SqlDataReader reader = rq.ExecuteReader();
                    while (reader.Read())
                    {
                        recup = reader[0].ToString();
                    }
                    sqlconnection.Close();
                    if (recup != "")
                    {
                        return Int32.Parse(recup);
                    }
                    else
                    {
                        return 0;

                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public List<Niveau> listNiveaux(int id)
        {
            List<Niveau> o = new List<Niveau>();

            //rempli la liste des niveaux
            Niveau n = new Niveau();
            for (int i = 1; i < 150; i++)
            {
                n.ID = i;
                n.description = "description defi du niveau" + i;
                o.Add(n);
            }
            return o;
        }
/*
        public void LastLevelDate(int id_joueur)
        {
            //refaire la requete
            string requeteInsertion =
                "update date_niv = "+DateTime.Today;
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");

            
        }

        public void SetLastConnection(int id_joueur)
        {
            //refaire la requete
            string requeteInsertion =
                "update last_con = " + DateTime.Today;
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");
        }

        public void setNiveauEnd(int lvl, int id_joueur)
        {
            //refaire la requete
            //requete de recuperation du champs niveau
            //requete d'update dans le champs niveau
            //recuperation des anciens niveaux
            //concatenation avec le nouveau niveau
            //update du champs niveaux avec les nouveaux niveaux mis a jour

            string requeteInsertion =
                "update date_niv = " + DateTime.Today;
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);
            Console.WriteLine("open cnt");

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
            Console.WriteLine("close cnt");
        }

        public int[] getNiveauEnd(int id_joueur)
        {
            int[] lvlend = null;
            //refaire la requete
            string requeteInsertion =
                "select end_niv from utilisateur where id = @idjoueur";
            string chaineConnection =
                "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                int i = 0;
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    lvlend[i] = (int) reader[0];
                    i++;
                }
                sqlconnection.Close();
                return lvlend;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public void UpdateScore(int id_joueur, int score)
        {
            //refaire la requete
            string requeteInsertion =
                "update score = @score From dbo.utilisateur Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@score", score);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
        }

        public void setEnLigne(int id_joueur)
        {
            //refaire la requete
            string requeteInsertion =
                "update connexion = 1 From dbo.User Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
        }

        public void setHorsLigne(int id_joueur)
        {
            
            //refaire la requete
            string requeteInsertion =
                "update connexion = 0 From dbo.User Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            int i = rq.ExecuteNonQuery();
            sqlconnection.Close();
        }

        public bool isEnLigne(int id_joueur)
        {
            bool co = false;
            string requeteInsertion ="select connexion From dbo.User Where id = @id_joueur";
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteInsertion, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    co = (bool)reader[0];
                }
                sqlconnection.Close();
                return co;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public string GetScore(int id_joueur)
        {
            string score = "";
            string requeteSelectScore = "select score From dbo.User Where id = @id_joueur";
            
            string chaineConnection = "Data Source=.\\sqlexpress;Initial Catalog=searchANDshoot;Integrated Security=True;Pooling=False";
            var sqlconnection = new SqlConnection(chaineConnection);

            var rq = new SqlCommand(requeteSelectScore, sqlconnection);
            rq.Parameters.AddWithValue("@id_joueur", id_joueur);
            sqlconnection.Open();
            try
            {
                SqlDataReader reader = rq.ExecuteReader();
                while (reader.Read())
                {
                    score = (string)reader[0];
                }
                sqlconnection.Close();
                return score;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }*/
    }
}
