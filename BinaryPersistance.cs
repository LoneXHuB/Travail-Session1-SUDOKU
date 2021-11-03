using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace LoneX.UQTR.Sudoku
{
    public class BinaryPersistance : FilePersistance 
    {
       #region PublicMethods
        public override void SaveGrid(BlocGrid _grid , string _fileName)
        {
            ///Sql
            var _formatter = new BinaryFormatter();
            var _path = Application.persistentDataPath +"/"+ _fileName + FILE_EXTENTION;
            FileStream _stream = new FileStream(_path , FileMode.Create);
            
            var _data = new GameData(_grid);
            _formatter.Serialize(_stream , _data);
            _stream.Close();
        }

        public override GameData LoadGrid(string _fileName)
        {
            var _path = Application.persistentDataPath +"/"+ _fileName + FILE_EXTENTION;
            if(File.Exists(_path))
            {
                var _formatter = new BinaryFormatter();
                var _stream = new FileStream(_path , FileMode.Open);

                var _data = _formatter.Deserialize(_stream) as GameData;
                _stream.Close();
                return _data;
            }else
                throw new System.ArgumentException("file does not exist");
        }

        public override IEnumerable<string> GetSavedGames()
        {
            DirectoryInfo _dir = new DirectoryInfo(Application.persistentDataPath +"/");
            FileInfo[] _files = _dir.GetFiles("*" + FILE_EXTENTION);
            
            foreach(FileInfo file in _files )
            { 
                var _extentionIndx = file.Name.Length -FILE_EXTENTION.Length;    
                yield return file.Name.Substring(0 , _extentionIndx);
            }
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}