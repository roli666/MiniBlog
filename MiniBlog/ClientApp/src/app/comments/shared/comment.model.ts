export interface Comment {
  Id: string;
  OwnerPostId: string;
  Parent: Comment;
  Children: Comment[];
  Content: string;
  OwnerUser: string;
  Depth: number;
}
