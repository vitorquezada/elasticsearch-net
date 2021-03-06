using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class ShingleTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/shingle-tokenfilter.asciidoc:31")]
		public void Line31()
		{
			// tag::c065a200c00e2005d88ec2f0c10c908a[]
			var response0 = new SearchResponse<object>();
			// end::c065a200c00e2005d88ec2f0c10c908a[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [ ""shingle"" ],
			  ""text"": ""quick brown fox jumps""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/shingle-tokenfilter.asciidoc:116")]
		public void Line116()
		{
			// tag::ac366b9dda7040e743dee85335354094[]
			var response0 = new SearchResponse<object>();
			// end::ac366b9dda7040e743dee85335354094[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""shingle"",
			      ""min_shingle_size"": 2,
			      ""max_shingle_size"": 3
			    }
			  ],
			  ""text"": ""quick brown fox jumps""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/shingle-tokenfilter.asciidoc:220")]
		public void Line220()
		{
			// tag::56fa6c9e08258157d445e2f92274962b[]
			var response0 = new SearchResponse<object>();
			// end::56fa6c9e08258157d445e2f92274962b[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""shingle"",
			      ""min_shingle_size"": 2,
			      ""max_shingle_size"": 3,
			      ""output_unigrams"": false
			    }
			  ],
			  ""text"": ""quick brown fox jumps""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/shingle-tokenfilter.asciidoc:298")]
		public void Line298()
		{
			// tag::9cd5719dfced4465c65547da938555f1[]
			var response0 = new SearchResponse<object>();
			// end::9cd5719dfced4465c65547da938555f1[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_shingle"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""shingle"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/shingle-tokenfilter.asciidoc:374")]
		public void Line374()
		{
			// tag::12ec704d62ffedcb03787e6aba69d382[]
			var response0 = new SearchResponse<object>();
			// end::12ec704d62ffedcb03787e6aba69d382[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""whitespace"",
			  ""filter"": [
			    {
			      ""type"": ""stop"",
			      ""stopwords"": [ ""a"" ]
			    },
			    {
			      ""type"": ""shingle"",
			      ""filler_token"": ""+""
			    }
			  ],
			  ""text"": ""fox jumps a lazy dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/shingle-tokenfilter.asciidoc:488")]
		public void Line488()
		{
			// tag::fc0e7a69e7d2807dc9db43b4fef68db8[]
			var response0 = new SearchResponse<object>();
			// end::fc0e7a69e7d2807dc9db43b4fef68db8[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""en"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [ ""my_shingle_filter"" ]
			        }
			      },
			      ""filter"": {
			        ""my_shingle_filter"": {
			          ""type"": ""shingle"",
			          ""min_shingle_size"": 2,
			          ""max_shingle_size"": 5,
			          ""output_unigrams"": false
			        }
			      }
			    }
			  }
			}");
		}
	}
}