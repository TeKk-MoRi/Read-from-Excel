using DataTransit.Common.Helper;
using DataTransit.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace Dapper.Helper
{
    public class DapperHelper : IDapperHelper
    {
        private IDbConnection _connection => new SqlConnection(ConfigurationManager.GetConnectionString("ConnStr"));

        public void BulkInser(List<ExcelModel> values)
        {
            using var connection = _connection;
            DapperPlusManager.Entity<ExcelModel>().Table("ExelModels");
            try
            {
                connection.Open();
                connection.BulkInsert(values);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

        }

        public int Execute(string sql, object param = null, CommandType? commandType = null)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return connection.Execute(sql, param, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, param, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public object QueryScalar(string sql, object param = null, CommandType? commandType = null)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return connection.ExecuteScalar(sql, param, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public async Task<object> QueryScalarAsync(string sql, object param = null, CommandType? commandType = null)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return await connection.ExecuteScalarAsync(sql, param, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return connection.Query<T>(sql, param, commandType: commandType);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return await connection.QueryAsync<T>(sql, param, commandType: commandType);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }


        }

        public IEnumerable<TReturn> Query<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, CommandType? commandType = null, bool buffered = true)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return connection.Query(sql, map, param, commandType: commandType, buffered: buffered);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }


        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map, object param = null, CommandType? commandType = null, bool buffered = true)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, commandType: commandType, buffered: buffered);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

        }

        public IEnumerable<TReturn> Query<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, CommandType? commandType = null, bool buffered = true)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return connection.Query(sql, map, param, commandType: commandType, buffered: buffered);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }


        }

        public async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql, Func<T1, T2, T3, TReturn> map, object param = null, CommandType? commandType = null, bool buffered = true)
        {
            using var connection = _connection;
            try
            {
                connection.Open();
                return await connection.QueryAsync(sql, map, param, commandType: commandType, buffered: buffered);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }



        }

        public int ExecuteTransaction(IEnumerable<SqlScript> scripts)
        {
            var connection = _connection;

            var count = 0;

            IDbTransaction tran = null;

            try
            {
                connection.Open();

                tran = connection.BeginTransaction();

                count += scripts.Sum(script => connection.Execute(script.Sql, script.Param, tran, commandType: script.CommandType));

                tran.Commit();

                return count;
            }
            catch
            {
                tran?.Rollback();

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public async Task<int> ExecuteTransactionAsync(IEnumerable<SqlScript> scripts)
        {
            var connection = _connection;

            var count = 0;

            IDbTransaction tran = null;

            try
            {
                connection.Open();

                tran = connection.BeginTransaction();

                foreach (var script in scripts)
                {
                    count += await connection.ExecuteAsync(script.Sql, script.Param, tran, commandType: script.CommandType);
                }

                tran.Commit();

                return count;
            }
            catch
            {
                tran?.Rollback();

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public void ExecuteTransaction(Action<IDbConnection> transaction)
        {
            var connection = _connection;

            IDbTransaction tran = null;
            try
            {
                connection.Open();

                tran = connection.BeginTransaction();

                if (transaction.Method.IsDefined(typeof(AsyncStateMachineAttribute), false))
                {
                    throw new NotSupportedException("asynchronous Action<IDbConnection> is not awaitable nor supported, please use Func<IDbConnection,Task> instead");
                }

                transaction(connection);

                tran.Commit();
            }
            catch
            {
                tran?.Rollback();

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public TResult ExecuteTransaction<TResult>(Func<IDbConnection, TResult> transaction)
        {
            var connection = _connection;

            IDbTransaction tran = null;

            var isAsync = transaction.Method.IsDefined(typeof(AsyncStateMachineAttribute), false);

            try
            {
                connection.Open();

                tran = connection.BeginTransaction();

                var result = transaction(connection);

                if (isAsync)
                {
                    (result as Task)?.Wait();
                }

                tran.Commit();

                return result;
            }
            catch (Exception e)
            {
                tran?.Rollback();

                if (isAsync && e.InnerException != null)
                {
                    throw e.InnerException;
                }

                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}