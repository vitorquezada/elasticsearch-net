﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatSnapshots
{
	public class CatSnapshotsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/snapshots/foo")
					.Fluent(c => c.Cat.Snapshots(s => s.RepositoryName("foo")))
					.Request(c => c.Cat.Snapshots(new CatSnapshotsRequest("foo")))
					.FluentAsync(c => c.Cat.SnapshotsAsync(s => s.RepositoryName("foo")))
					.RequestAsync(c => c.Cat.SnapshotsAsync(new CatSnapshotsRequest("foo")))
				;

			await GET("/_cat/snapshots/foo?v=true")
					.Fluent(c => c.Cat.Snapshots(s => s.RepositoryName("foo").Verbose()))
					.Request(c => c.Cat.Snapshots(new CatSnapshotsRequest("foo") { Verbose = true }))
					.FluentAsync(c => c.Cat.SnapshotsAsync(s => s.RepositoryName("foo").Verbose()))
					.RequestAsync(c => c.Cat.SnapshotsAsync(new CatSnapshotsRequest("foo") { Verbose = true }))
				;
		}
	}
}
