using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DefectReport
{
    public class DataAccess
    {
        readonly SQLiteAsyncConnection database;

        #region Create Connection

        public DataAccess(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<DefectReportItem>().Wait();
        }

        #endregion

        #region Get All

        public Task<List<DefectReportItem>> GetItemsAsync()
        {
            return database.Table<DefectReportItem>().ToListAsync();
        }

        public Task<List<DefectReportItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<DefectReportItem>("SELECT * FROM [DefectReportItem]");
        }

        #endregion

        #region Get Defect List

        public Task<List<DefectList>> GetSelectedDefects()
        {
            //return defect List
            return database.QueryAsync<DefectList>("SELECT UNIQUE [defect] FROM [DefectReportItem] WHERE [useDefect] = 1");
        }

        public Task<int> UpdateDefectListAsync(DefectReportItem newDefect, bool Flag)
        {
            //update defects
            return database.UpdateAsync("UPDATE [DefectReportItem] SET [useDefect] = " + Flag + " WHERE [Defect] = " + newDefect.Defect);
        }

        #endregion

        #region Individual Defect Items

        public Task<DefectReportItem> GetItemAsync(int id)
        {
            //Get specific defect
            return database.Table<DefectReportItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(DefectReportItem newDefect)
        {
            //Save new item
            if (newDefect.Id != 0)
            {
                return database.UpdateAsync(newDefect);
            }
            else
            {
                return database.InsertAsync(newDefect);
            }
        }

        public Task<int> DeleteItemAsync(DefectReportItem newDefect)
        {
            //Delete item
            return database.DeleteAsync(newDefect);
        }

        #endregion
    }
}

