using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace WS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(string value);
        //operation de test d'insert dans la bdd de test
      /*  [OperationContract]
        string SetTest(string test);
        
        [OperationContract]
        string GetTest();
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="mail"></param>
        /// <param name="mdp"></param>
        /// <param name="date_inscription"></param>
        /// <param name="date_niv"></param>
        /// <param name="last_con"></param>
        /// <param name="niveau_finis"></param>
        /// <param name="score"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        [OperationContract]
        int Inscription(string password, string login, string mail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peudo"></param>
        /// <param name="email"></param>
        /// <param name="mdp"></param>
        /// <returns></returns> 
        [OperationContract]
        int Authentification(string login, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="login"></param>
        /// <param name="ville"></param>
        /// <param name="id"></param>
        /// <returns></returns> 
        [OperationContract]
        string SetInfos(string nom, string prenom, string login, string ville, int id);

        [OperationContract]
        info GetInfos(int id_joueur);

        [OperationContract]
        personnes GetPeopleInfos();

        [OperationContract]
        personnes GetFriendInfos(int id);

        [OperationContract]
        int GetScore(int id);

        [OperationContract]
        int GetUserId(string mail);

        [OperationContract]
        user GetUserInfos(int id);

        [OperationContract]
        bool SetLastCon(int id);

        [OperationContract]
        int InscriptionSocial(string login, string nom, string prenom, string mail, byte[] picture, int connexion,
            string token);

        [OperationContract]
        int GetUserIdTwitter(string twitter);

        [OperationContract]
        int InscriptionTwitter(string login, byte[] picture, int connexion, string token);

        [OperationContract]
        byte[] getPic(byte[] pic);

        [OperationContract]
        List<Niveau> listNiveaux(int id);

        /*
        /// <summary>
        /// met a jour la date de fin du niveau
        /// </summary>
        /// <param name="id_joueur"></param>
        [OperationContract]
        void LastLevelDate(int id_joueur);

        /// <summary>
        /// met a jour la date de derniere connection
        /// </summary>
        /// <param name="id_joueur"></param>
        [OperationContract]
        void SetLastConnection(int id_joueur);

        /// <summary>
        /// ajoute le niveau fini
        /// </summary>
        /// <param name="id_joueur"></param>
        [OperationContract]
        void setNiveauEnd(int lvl, int id_joueur);

        /// <summary>
        /// renvoie les dernier niveaus du joueur
        /// </summary>
        /// <param name="id_joueur"></param>

        /// <returns>numeros des niveau dans un int[]</returns>
        [OperationContract]
        int[] getNiveauEnd(int id_joueur);

        /// <summary>
        /// met a jour le score du joueur
        /// </summary>
        /// <param name="id_joueur"></param>
        /// <param name="score"></param>
        [OperationContract]
        void UpdateScore(int id_joueur, int score);

        /// <summary>
        /// met a jour l'onformation que le joueur est en ligne 
        /// </summary>
        /// <param name="id_joueur"></param>
        [OperationContract]
        void setEnLigne(int id_joueur);

        /// <summary>
        /// met a jour l'onformation que le joueur est en ligne 
        /// </summary>
        /// <param name="id_joueur"></param>
        [OperationContract]
        void setHorsLigne(int id_joueur);
        /// <summary>
        ///  demande si le joueur est en ligne
        /// </summary>
        /// <param name="id_joueur"></param>
        /// <returns>return un booleen si le joueur est connecté ou pas</returns>
        [OperationContract]
        bool isEnLigne(int id_joueur);

        /// <summary>
        /// recupere le score du joueur
        /// </summary>
        /// <param name="id_joueur"></param>
        /// <returns>le score du joueur</returns>
        [OperationContract]
        string GetScore(int id_joueur);
        */
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class info
    {
        public info()
        {
        }
        [DataMember] public string nom;
        [DataMember] public string prenom;
        [DataMember] public string login;
        [DataMember] public string ville;
    }


    [DataContract]
    public class user
    {
        public user()
        {
        }

        [DataMember]
        public string token;
        [DataMember]
        public string login;
    }

    [DataContract]
    public class personnes
    {
        public personnes()
        {
        }

        [DataMember] public List<int>id;
        [DataMember] public List<string>login;
        [DataMember] public List<int> score;
        [DataMember] public List<DateTime> date_insc;
        [DataMember] public List<byte[]> img;
        /*[DataMember] public int id;
        [DataMember] public string login;
        [DataMember] public int score;
        [DataMember] public DateTime date_insc;
        [DataMember] public byte[] img;*/

    }

    [DataContract]
    public class Niveau
    {
        [DataMember]
        public int ID;
        [DataMember]
        public string description;

    }
}
