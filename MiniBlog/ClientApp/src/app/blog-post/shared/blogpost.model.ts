export interface BlogPost {
  Id: string;
  Title: string;
  Content: string;
  CreatedBy: string;
  Category: string;
  BackgroundImage: string;
  Comments: Comment;
  AllowedAges: string[];
  CreatedOn: Date;
}
