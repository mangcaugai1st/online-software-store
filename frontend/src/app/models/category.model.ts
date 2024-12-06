// export class Category {
//   id: number;
//   name: string;
//   description: string;
//
//   constructor(id: number, name: string, description: string) {
//       this.id = id;
//       this.name = name;
//       this.description = description;
//   }
// }

export interface Category {
  id: number;
  name: string;
  slug: string;
  description: string;
}
