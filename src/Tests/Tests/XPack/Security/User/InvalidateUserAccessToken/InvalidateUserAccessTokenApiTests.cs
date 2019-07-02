﻿using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Managed.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.User.InvalidateUserAccessToken
{
	[SkipVersion("<5.5.0", "")]
	public class InvalidateUserAccessTokenApiTests
		: ApiIntegrationTestBase<XPackCluster, InvalidateUserAccessTokenResponse, IInvalidateUserAccessTokenRequest,
			InvalidateUserAccessTokenDescriptor, InvalidateUserAccessTokenRequest>
	{
		protected const string AccessTokenValueKey = "accesstoken";

		public InvalidateUserAccessTokenApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected virtual string CurrentAccessToken => RanIntegrationSetup ? ExtendedValue<string>(AccessTokenValueKey) : "foo";

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			token = CurrentAccessToken
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> Fluent => d => d;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override InvalidateUserAccessTokenRequest Initializer => new InvalidateUserAccessTokenRequest(CurrentAccessToken);

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => "/_security/oauth2/token";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var r = client.Security.GetUserAccessToken(Admin.Username, Admin.Password);
			r.ShouldBeValid();
			ExtendedValue(AccessTokenValueKey, r.AccessToken);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.InvalidateUserAccessToken(CurrentAccessToken, f),
			(client, f) => client.Security.InvalidateUserAccessTokenAsync(CurrentAccessToken, f),
			(client, r) => client.Security.InvalidateUserAccessToken(r),
			(client, r) => client.Security.InvalidateUserAccessTokenAsync(r)
		);

		protected override InvalidateUserAccessTokenDescriptor NewDescriptor() => new InvalidateUserAccessTokenDescriptor(CurrentAccessToken);

		protected override void ExpectResponse(InvalidateUserAccessTokenResponse response) => response.InvalidatedTokens.Should().BeGreaterThan(0);
	}

	public class InvalidateUserAccessTokenBadPasswordApiTests : InvalidateUserAccessTokenApiTests
	{
		public InvalidateUserAccessTokenBadPasswordApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string CurrentAccessToken => "bad_password";

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 401;

		protected override void ExpectResponse(InvalidateUserAccessTokenResponse response) => response.ServerError.Should().NotBeNull();
	}
}
