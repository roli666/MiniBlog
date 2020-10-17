export interface BlogPost {
  id: string;
  title: string;
  content: string;
  createdBy: string;
  category: string;
  backgroundImage: string;
  comments: Comment[];
  allowedAges: string[];
  createdOn: Date;
  commentCount: number;
}
