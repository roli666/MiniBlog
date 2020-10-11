export interface BlogPost {
  Title: string;
  Content: string;
  CreatedBy: string;
  BackgroundImage: Image;
  AllowedAge: string[];
  CreatedOn: Date;
}
export interface Image {
  ImageName: string;
  ImagePath: string;
}
