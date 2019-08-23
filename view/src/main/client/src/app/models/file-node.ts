export interface FileNode {
  exists(): boolean;
  getName(): string;
  getPath(): string;
}
