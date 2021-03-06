﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using cd.BLL;
using cd.Model;

namespace cd.Module.Admin.Controllers {
	[Route("[controller]")]
	[Obsolete]
	public class SysController : Controller {
		[HttpGet(@"connection")]
		public object Get_connection() {
			List<Hashtable> ret = new List<Hashtable>();
			foreach (var conn in SqlHelper.Pool.AllConnections) {
				ret.Add(new Hashtable() {
						{ "数据库", conn.SqlConnection.Database },
						{ "状态", conn.SqlConnection.State },
						{ "最后活动", conn.LastActive },
						{ "获取次数", conn.UseSum }
					});
			}
			return new {
				FreeConnections = SqlHelper.Pool.FreeConnections.Count,
				AllConnections = SqlHelper.Pool.AllConnections.Count,
				GetConnectionQueue = SqlHelper.Pool.GetConnectionQueue.Count,
				GetConnectionAsyncQueue = SqlHelper.Pool.GetConnectionAsyncQueue.Count,
				List = ret
			};
		}
		[HttpGet(@"connection/redis")]
		public object Get_connection_redis() {
			var ret = new Hashtable();
			foreach(var pool in RedisHelper.ClusterNodes) {
				List<Hashtable> list = new List<Hashtable>();
				foreach (var conn in pool.Value.AllConnections) {
					list.Add(new Hashtable() {
						{ "最后活动", conn.LastActive },
						{ "获取次数", conn.UseSum }
					});
				}
				ret.Add(pool.Key, new {
					FreeConnections = pool.Value.FreeConnections.Count,
					AllConnections = pool.Value.AllConnections.Count,
					GetConnectionQueue = pool.Value.GetConnectionQueue.Count,
					GetConnectionAsyncQueue = pool.Value.GetConnectionAsyncQueue.Count,
					List = list
				});
			}
			return ret;
		}

		[HttpGet(@"init_sysdir")]
		public APIReturn Get_init_sysdir() {
			/*
			if (Sysdir.SelectByParent_id(null).Count() > 0)
				return new APIReturn(-33, "本系统已经初始化过，页面没经过任何操作退出。");

			SysdirInfo dir1, dir2, dir3;
			dir1 = Sysdir.Insert(null, DateTime.Now, "运营管理", 1, null);

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "post", 1, "/post/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/post/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/post/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/post/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/post/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "song", 2, "/song/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/song/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/song/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/song/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/song/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "song_tag", 3, "/song_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/song_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/song_tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/song_tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/song_tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tag", 4, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "topic", 5, "/topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/topic/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/topic/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/topic/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/topic/del");
			*/
			return new APIReturn(0, "管理目录已初始化完成。");
		}
	}
}
