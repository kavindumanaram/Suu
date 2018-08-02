/*create table UserMention (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [screen_name] [varchar](50) NULL,
    [name] [varchar](50) NULL,
    [id_str] [varchar](50) NULL,
    [indices] [int] NOT NULL,
CONSTRAINT [PK_UserMention] PRIMARY KEY CLUSTERED
   (
      [Id] asc
   )
)*/

/*create table Url (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [url] [varchar](50) NULL,
    [expanded_url] [varchar](50) NULL,
    [display_url] [varchar](50) NULL,
    [indices] [int] NOT NULL,
CONSTRAINT [PK_Url] PRIMARY KEY CLUSTERED
   (
      [Id] asc
   )
)
*/

create table [User] (
    [Id] [bigint] primary key NOT NULL,
    [name] [nvarchar](50) COLLATE Latin1_General_CI_AI NULL,
    [screen_name] [nvarchar](50) COLLATE Latin1_General_CI_AI NULL,
    [location] [varchar](50) NULL,
    [description] [nvarchar](max) NULL,
    [url] [varchar](50) NULL,
    [protected] bit  NULL,
    [followers_count] [int]  NULL,
    [friends_count] [int]  NULL,
    [listed_count] [int]  NULL,
    [created_at] [varchar](50) NULL,
    [favourites_count] [int]  NULL,
    [utc_offset] nVarchar(50) ,
    [time_zone] nVarchar(50) ,
    [geo_enabled] bit  NULL,
    [verified] bit  NULL,
    [statuses_count] [int]  NULL,
    [lang] [varchar](50) NULL,
    [contributors_enabled] bit  NULL,
    [is_translator] bit  NULL,
    [is_translation_enabled] bit  NULL,
    [profile_background_color] [varchar](50) NULL,
    [profile_background_image_url] [varchar](500) NULL,
    [profile_background_image_url_https] [varchar](500) NULL,
    [profile_background_tile] bit  NULL,
    [profile_image_url] [varchar](500) NULL,
    [profile_image_url_https] [varchar](500) NULL,
    [profile_banner_url] [varchar](500) NULL,
    [profile_link_color] [varchar](50) NULL,
    [profile_sidebar_border_color] [varchar](50) NULL,
    [profile_sidebar_fill_color] [varchar](50) NULL,
    [profile_text_color] [varchar](50) NULL,
    [profile_use_background_image] bit  NULL,
    [has_extended_profile] bit  NULL,
    [default_profile] bit  NULL,
    [default_profile_image] bit  NULL,
    [following] bit  NULL,
    [follow_request_sent] bit  NULL,
    [notifications] bit  NULL,
    [translator_type] [varchar](50) NULL,
	--retweeted_status_id [bigint] UNIQUE FOREIGN KEY REFERENCES [Status](Id)
--CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
--   (
--      [Id] asc
--   )
)


create table [Status] (
    [Id] [bigint] primary key NOT NULL,
    [created_at] [varchar](50) NULL,
    [text] [nvarchar](300)  COLLATE Latin1_General_CI_AI NULL,
    [truncated] bit  NULL,
    --[metadata] int,
    [source] nvarchar(max) NULL,
    [in_reply_to_status_id] [bigint] NULL,
    [in_reply_to_user_id] [bigint] NULL,
    [in_reply_to_screen_name] [nvarchar](50) COLLATE Latin1_General_CI_AI NULL,
   -- [user] int,
    [geo]  [varchar](50) NULL,
    [coordinates]  [varchar](50) NULL,
    [place]  [varchar](50) NULL,
    [contributors]  [varchar](50) NULL,
    [retweeted_status]  [varchar](50),
    [is_quote_status] bit  NULL,
    [retweet_count] [int]  NULL,
    [favorite_count] [int]  NULL,
    [favorited] bit  NULL,
    [retweeted] bit  NULL,
    [lang] nVarchar(50) NULL,
    [possibly_sensitive] bit NULL,
	[user_id] [bigint] FOREIGN KEY REFERENCES [User](Id)
--CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED
 --  (
 --     [Id] asc
 --  )
)


create table Metadata (
    [Id] [int] IDENTITY(1,1) Primary key NOT NULL,
    [iso_language_code] [varchar](50) NULL,
    [result_type] [varchar](50) NULL,
	status_id   [bigint] FOREIGN KEY REFERENCES [Status](Id)
/*CONSTRAINT [PK_Metadata] PRIMARY KEY CLUSTERED
   (
      [Id] asc
   )*/
)

/*create table RetweetedStatus (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [created_at] [varchar](50) NULL,
    [id_str] [varchar](50) NULL,
    [text] [varchar](50) NULL,
    [truncated] bit NOT NULL,
    [source] [varchar](50) NULL,
    [in_reply_to_status_id] [bigint] NULL,
    [in_reply_to_status_id_str] [varchar](50) NULL,
    [in_reply_to_user_id] [bigint] NULL,
    [in_reply_to_user_id_str] [varchar](50) NULL,
    [in_reply_to_screen_name] [varchar](50) NULL,
    [user] int,
    [geo] nVarchar(50) NULL,
    [coordinates] nVarchar(50) NULL,
    [place] nVarchar(50) NULL,
    [contributors] nVarchar(50) NULL,
    [is_quote_status] bit NOT NULL,
    [retweet_count] [int] NOT NULL,
    [favorite_count] [int] NOT NULL,
    [favorited] bit NOT NULL,
    [retweeted] bit NOT NULL,
    [possibly_sensitive] bit NOT NULL,
    [lang] [varchar](50) NULL,
CONSTRAINT [PK_RetweetedStatus] PRIMARY KEY CLUSTERED
   (
      [Id] asc
   )
)
*/


/*
create table SearchMetadata (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [completed_in] [decimal](9,2) NOT NULL,
    [max_id] [bigint] NOT NULL,
    [max_id_str] [varchar](50) NULL,
    [query] [varchar](50) NULL,
    [refresh_url] [varchar](50) NULL,
    [count] [int] NOT NULL,
    [since_id] [int] NOT NULL,
    [since_id_str] [varchar](50) NULL,
CONSTRAINT [PK_SearchMetadata] PRIMARY KEY CLUSTERED
   (\
      [Id] asc
   )
)*/