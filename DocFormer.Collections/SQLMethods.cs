using DocFormer.Core.Interfaces;
using DocFormer.Core.Models;
using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DocFormer
{
    public abstract class SQLMethods : PropertyChangedRealization
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SQLMethods()
        {
        }

        public void SelectCollections()
        {
            try
            {
                using (SQLiteConnection connection = SqlLiteConnection())
                {
                    Organizations = connection.Query<Organizations>("SELECT * FROM Organizations").ToList();
                    Customers = connection.Query<Customers>("SELECT * FROM Customers").ToList();
                    Objects = connection.Query<Objects>("SELECT * FROM Objects").ToList();
                    Technologies = connection.Query<TechnologyModel>("SELECT * FROM TechnologyList").ToList();
                    CustomerType = connection.Query<CustomerType>("SELECT * FROM CustomerType").ToDictionary(k => k.Id, v => v.Type);
                    TechnologyType = connection.Query<TechnologyType>("SELECT * FROM TechnologyType").ToDictionary(k => k.Id, v => v.Type);
                    CountType = connection.Query<TechnologyCountType>("SELECT * FROM TechnologyCountType").ToDictionary(k => k.Id, v => v.Type);
                    DocNames = connection.Query<DocumentsNames>("SELECT * FROM DocumentsNames").ToList();
                    DocTemplates = connection.Query<DocumentsTemplates>("SELECT * FROM DocumentsTemplates").ToList();
                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }


        #region Select
        public void SyncCollection(ObservableCollection<Organizations> col)
        {
            try
            {
                if (Organizations.Count() < col.Count)
                {
                    var ids = col.Except(Organizations).ToList();
                    for(int i = ids.Count()-1; i >= 0; i--)
                    {
                        col.Remove(ids[i]);
                    }
                }
                for (int i = 0; i < Organizations.Count; i++)
                {
                    var o = col.Where(item => item.Id == Organizations[i].Id).FirstOrDefault();
                    if (o == null)
                    {
                        col.Add(Organizations[i]);
                    }
                    else
                    {
                        o.Update(Organizations[i]);
                    }
                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        public void SyncCollection(ObservableCollection<Customers> col)
        {
            try
            {
                if (Customers.Count() < col.Count)
                {
                    var ids = col.Except(Customers).ToList();
                    for (int i = ids.Count() - 1; i >= 0; i--)
                    {
                        col.Remove(ids[i]);
                    }
                }
                for (int i = 0; i < Customers.Count; i++)
                {
                    var o = col.Where(item => item.Id == Customers[i].Id).FirstOrDefault();
                    if (o == null)
                    {
                        col.Add(Customers[i]);
                    }
                    else
                    {
                        o.Update(Customers[i]);
                    }
                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        public void SyncCollection(ObservableCollection<Objects> col)
        {
            try
            {
                if (Objects.Count() < col.Count)
                {
                    var ids = col.Except(Objects).ToList();
                    for (int i = ids.Count() - 1; i >= 0; i--)
                    {
                        col.Remove(ids[i]);
                    }
                }
                for (int i = 0; i < Objects.Count; i++)
                {
                    var o = col.Where(item => item.Id == Objects[i].Id).FirstOrDefault();
                    if (o == null)
                    {
                        col.Add(Objects[i]);
                    }
                    else
                    {
                        o.Update(Objects[i]);
                    }
                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        public void SyncCollection(ObservableCollection<TechnologyModel> col)
        {
            try
            {
                if (Technologies.Count() < col.Count)
                {
                    var ids = col.Except(Technologies).ToList(); ;
                    for (int i = ids.Count() - 1; i >= 0; i--)
                    {
                        col.Remove(ids[i]);
                    }
                }
                for (int i = 0; i < Technologies.Count; i++)
                {
                    var o = col.Where(item => item.Id == Technologies[i].Id).FirstOrDefault();
                    if (o == null)
                    {
                        col.Add(Technologies[i]);
                    }
                    else
                    {
                        o.Update(Technologies[i]);
                    }
                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion

        #region Insert - Update
        /// <summary>
        /// Обновление или добавление организации
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public string InsertOrUpdate(Organizations org)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                DynamicParameters parametres = new DynamicParameters();
                parametres.Add("@Id", org.Id, System.Data.DbType.String);
                parametres.Add("@Name", org.Name.Trim(), System.Data.DbType.String);
                parametres.Add("@Address", string.IsNullOrEmpty(org.Address) ? (object)DBNull.Value : org.Address.Trim(), System.Data.DbType.String);
                parametres.Add("@LicenseNumber", string.IsNullOrEmpty(org.LicenseNumber) ? (object)DBNull.Value : org.LicenseNumber.Trim(), System.Data.DbType.String);
                parametres.Add("@LicenseValidityPeriod", string.IsNullOrEmpty(org.LicenseValidityPeriod) ? (object)DBNull.Value : org.LicenseValidityPeriod.Trim(), System.Data.DbType.String);
                parametres.Add("@LicenseIssued", string.IsNullOrEmpty(org.LicenseIssued) ? (object)DBNull.Value : org.LicenseIssued.Trim(), System.Data.DbType.String);
                parametres.Add("@Ogrn", string.IsNullOrEmpty(org.Ogrn) ? (object)DBNull.Value : org.Ogrn.Trim(), System.Data.DbType.String);
                parametres.Add("@Inn", string.IsNullOrEmpty(org.Inn) ? (object)DBNull.Value : org.Inn.Trim(), System.Data.DbType.String);
                parametres.Add("@Phone", string.IsNullOrEmpty(org.Phone) ? (object)DBNull.Value : org.Phone.Trim(), System.Data.DbType.String);

                var o = Organizations.Where(item => item.Id == org.Id).FirstOrDefault();
                if (o == null)
                {
                    var name = Organizations.Where(item => item.Name.Trim() == org.Name.Trim()).FirstOrDefault();
                    if (name != null)
                    {
                        result = $"Ошибка! организация с именем {org.Name} уже существует!";
                        throw new Exception(result);
                    }

                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        var qwery = $"INSERT INTO Organizations (Id, Name, Address, LicenseNumber, LicenseValidityPeriod, LicenseIssued, Ogrn, Inn, Phone) VALUES (@Id, @Name, @Address, @LicenseNumber, @LicenseValidityPeriod, @LicenseIssued, @Ogrn, @Inn, @Phone)";
                        connection.Query<int>(qwery, parametres);
                        Organizations = connection.Query<Organizations>("SELECT * FROM Organizations").ToList();
                    }
                    result = $"Организация {org.Name} успешно добавлена в базу!";
                }
                else
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        string qwery = $"UPDATE Organizations SET Name = @Name, Address = @Address, LicenseNumber = @LicenseNumber, LicenseValidityPeriod = @LicenseValidityPeriod, LicenseIssued = @LicenseIssued, Ogrn = @Ogrn, Inn = @Inn, Phone = @Phone WHERE Id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Organizations = connection.Query<Organizations>("SELECT * FROM Organizations").ToList();
                    }
                    result = $"Запись {org.Name} успешно обновлена";
                }
                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + Environment.NewLine + ex;
            }
        }

        public string InsertOrUpdate(Customers c)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                DynamicParameters parametres = new DynamicParameters();
                parametres.Add("@Id", c.Id, System.Data.DbType.String);
                parametres.Add("@Type", c.Type , System.Data.DbType.String);
                parametres.Add("@Organization", c.Organization == Guid.Empty ? (object)DBNull.Value : c.Organization, System.Data.DbType.String);
                parametres.Add("@FIO", string.IsNullOrEmpty(c.FIO) ? (object)DBNull.Value : c.FIO.Trim(), System.Data.DbType.String); ;
                parametres.Add("@Post", string.IsNullOrEmpty(c.Post) ? (object)DBNull.Value : c.Post.Trim(), System.Data.DbType.String);

                var o = Customers.Where(item => item.Id == c.Id).FirstOrDefault();
                if (o == null)
                {
                    var fioAndType = Customers.Where(item => (item.FIO == c.FIO) && (item.Type == c.Type)).FirstOrDefault();
                    if (fioAndType != null)
                    {
                        result = $"Ошибка! {c.FIO} уже существует!";
                        throw new Exception(result);
                    }

                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        string qwery = "INSERT INTO Customers (Id, Type, Organization, FIO, Post) VALUES (@Id, @Type, @Organization, @FIO, @Post)";
                        connection.Query<int>(qwery, parametres);
                        Customers = connection.Query<Customers>("SELECT * FROM Customers").ToList();
                    }
                    result = $"{c.FIO} успешно добавлен в базу!";
                }
                else
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        string qwery = $"UPDATE Customers SET Type = @Type, Organization = @Organization, FIO = @FIO, Post = @Post WHERE Id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Customers = connection.Query<Customers>("SELECT * FROM Customers").ToList();
                    }
                    result = $"Запись {c.FIO} успешно обновлена";
                }
                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + Environment.NewLine + ex;
            }
        }

        public string InsertOrUpdate(Objects obj)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                DynamicParameters parametres = new DynamicParameters();
                parametres.Add("@Id", obj.Id, System.Data.DbType.String);
                parametres.Add("@ObjectName", string.IsNullOrEmpty(obj.ObjectName) ? (object)DBNull.Value : obj.ObjectName.Trim(), System.Data.DbType.String);
                parametres.Add("@ObjectAdresse", string.IsNullOrEmpty(obj.ObjectAdresse) ? (object)DBNull.Value : obj.ObjectAdresse.Trim(), System.Data.DbType.String);

                var o = Objects.Where(item => item.Id == obj.Id).FirstOrDefault();
                if (o == null)
                {
                    var name = Objects.Where(item => item.ObjectName.Trim() == obj.ObjectName.Trim()).FirstOrDefault();
                    if (name != null)
                    {
                        result = $"Ошибка! объект {obj.ObjectName} уже существует!";
                        throw new Exception(result);
                    }

                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        string qwery = "INSERT INTO Objects (Id, ObjectName, ObjectAdresse) VALUES (@Id, @ObjectName, @ObjectAdresse)";
                        connection.Query<int>(qwery, parametres);
                        Objects = connection.Query<Objects>("SELECT * FROM Objects").ToList();
                    }
                    result = $"Объект {obj.ObjectName} успешно добавлен в базу!";
                }
                else
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();

                        string qwery = $"UPDATE Objects SET ObjectName = @ObjectName, ObjectAdresse = @ObjectAdresse WHERE Id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Objects = connection.Query<Objects>("SELECT * FROM Objects").ToList();
                    }
                    result = $"Запись {obj.ObjectName} успешно обновлена";
                }
                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + Environment.NewLine + ex;
            }
        }

        public string InsertOrUpdate(TechnologyModel tech)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                DynamicParameters parametres = new DynamicParameters();
                parametres.Add("@Id", tech.Id, System.Data.DbType.String);
                parametres.Add("@CountType", tech.CountType == Guid.Empty ? (object)DBNull.Value : tech.CountType, System.Data.DbType.String);
                parametres.Add("@TechnologyType", tech.TechnologyType == Guid.Empty ? (object)DBNull.Value : tech.TechnologyType, System.Data.DbType.String);
                parametres.Add("@TechnologyName", string.IsNullOrEmpty(tech.TechnologyName) ? (object)DBNull.Value : tech.TechnologyName.Trim(), System.Data.DbType.String);
                parametres.Add("@TechnologyMark", string.IsNullOrEmpty(tech.TechnologyMark) ? (object)DBNull.Value : tech.TechnologyMark.Trim(), System.Data.DbType.String);
                parametres.Add("@TechnologyCreator", string.IsNullOrEmpty(tech.TechnologyCreator) ? (object)DBNull.Value : tech.TechnologyCreator.Trim(), System.Data.DbType.String);
                parametres.Add("@Count", tech.Count, System.Data.DbType.String);
                parametres.Add("@Certificate", string.IsNullOrEmpty(tech.Certificate) ? (object)DBNull.Value : tech.Certificate.Trim(), System.Data.DbType.String);
                parametres.Add("@CertificateValidityPeriod", string.IsNullOrEmpty(tech.CertificateValidityPeriod) ? (object)DBNull.Value : tech.CertificateValidityPeriod.Trim(), System.Data.DbType.String);

                var o = Technologies.Where(item => item.Id == tech.Id).FirstOrDefault();
                if (o == null)
                {
                    var nameandmark = Technologies.Where(item => ((item.TechnologyName.Trim() == tech.TechnologyName.Trim()))
                                                              && ((item.TechnologyMark.Trim() == tech.TechnologyMark.Trim())
                                                              && (item.TechnologyType == tech.TechnologyType))).FirstOrDefault();
                    if (nameandmark != null)
                    {
                        result = $"Ошибка! запись {tech.TechnologyName} {tech.TechnologyMark} уже существует!";
                        throw new Exception(result);
                    }
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        string qwery = "INSERT INTO TechnologyList (Id, TechnologyType, CountType, TechnologyName, TechnologyMark, TechnologyCreator, Count, Certificate, CertificateValidityPeriod) VALUES (@Id, @TechnologyType, @CountType, @TechnologyName, @TechnologyMark, @TechnologyCreator, @Count, @Certificate, @CertificateValidityPeriod)";
                        connection.Query<int>(qwery, parametres);
                    
                        Technologies = connection.Query<TechnologyModel>("SELECT * FROM TechnologyList").ToList();
                    }
                    result = $"Техническое средство {tech.TechnologyName} успешно добавлено в базу!";
                }
                else
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                       

                        string qwery = $"UPDATE TechnologyList SET TechnologyType = @TechnologyType, CountType = @CountType, TechnologyName = @TechnologyName, TechnologyMark = @TechnologyMark, TechnologyCreator = @TechnologyCreator, Count = @Count, Certificate = @Certificate, CertificateValidityPeriod = @CertificateValidityPeriod  WHERE Id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Technologies = connection.Query<TechnologyModel>("SELECT * FROM TechnologyList").ToList();
                    }
                    result = $"Запись {tech.TechnologyName} успешно обновлена";
                }
                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + Environment.NewLine + ex;
            }
        }
        #endregion

        #region Delete
        public string Delete(Organizations org)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                var o = Organizations.Where(item => item.Id == org.Id).FirstOrDefault();
                if (o != null)
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        DynamicParameters parametres = new DynamicParameters();
                        parametres.Add("@Id", org.Id, System.Data.DbType.String);
                        string qwery = "DELETE FROM Organizations WHERE id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Organizations = connection.Query<Organizations>("SELECT * FROM Organizations").ToList();
                    }
                    result = $"Организация {org.Name} успешно удалена из базы";
                }

                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + ex;
            }
        }

        public string Delete(Customers c)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                var o = Customers.Where(item => item.Id == c.Id).FirstOrDefault();
                if (o != null)
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        DynamicParameters parametres = new DynamicParameters();
                        parametres.Add("@Id", c.Id, System.Data.DbType.String);
                        string qwery = "DELETE FROM Customers WHERE id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Customers = connection.Query<Customers>("SELECT * FROM Customers").ToList();
                    }
                    result = $"Субъект {c.FIO} успешно удален из базы";
                }

                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + ex;
            }
        }
       
        public string Delete(Objects obj)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                var o = Objects.Where(item => item.Id == obj.Id).FirstOrDefault();
                if (o != null)
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        DynamicParameters parametres = new DynamicParameters();
                        parametres.Add("@Id", obj.Id, System.Data.DbType.String);
                        string qwery = "DELETE FROM Objects WHERE id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Objects = connection.Query<Objects>("SELECT * FROM Objects").ToList();
                    }
                    result = $"Объект {obj.ObjectName} успешно удален из базы";
                }

                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + ex;
            }
        }

        public string Delete(TechnologyModel tech)
        {
            string result = "Ошибка! для подробностей вы можете ознакомиться с log файлом - ";
            try
            {
                var o = Technologies.Where(item => item.Id == tech.Id).FirstOrDefault();
                if (o != null)
                {
                    using (SQLiteConnection connection = SqlLiteConnection())
                    {
                        connection.Open();
                        DynamicParameters parametres = new DynamicParameters();
                        parametres.Add("@Id", tech.Id, System.Data.DbType.String);
                        string qwery = "DELETE FROM TechnologyList WHERE id = @Id";
                        connection.Query<int>(qwery, parametres);
                        Technologies = connection.Query<TechnologyModel>("SELECT * FROM TechnologyList").ToList();
                    }
                    result = $"Оборудование {tech.TechnologyName} успешно удалено из базы";
                }

                return result;

            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                logger.Info(result);
                return result + ex;
            }
        }


        #endregion

        private SQLiteConnection SqlLiteConnection()
        {
            return new SQLiteConnection("Data Source=DocFormerBase.db");
        }
        
        public List<Organizations> Organizations { get; set; }
        public Dictionary<Guid, string> CustomerType { get; set; }
        public Dictionary<Guid, string> TechnologyType { get; set; }
        public Dictionary<Guid, string> CountType { get; set; }
        public List<TechnologyModel> Technologies { get; set; }
        public List<Customers> Customers { get; set; }
        public List<Objects> Objects { get; set; }
        public List<DocumentsNames> DocNames { get; set; }
        public List<DocumentsTemplates> DocTemplates { get; set; }
    }
}
