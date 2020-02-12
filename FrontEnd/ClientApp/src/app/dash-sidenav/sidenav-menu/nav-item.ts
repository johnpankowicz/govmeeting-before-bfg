/*
* Since a NavItem can have an array of child NavItems, it is a tree object.
* An array of NavItems is an array of trees.
* The 'position' property is its position within an array of NavItems.
* All counting starts with '0';
* [0]     = 1st NavItem in top array
* [1,2]   = 2nd NavItem's 3rd child.
* [0,3,1]   = 1st NavItem's 4th child's 2nd child.
*/


export class NavItem {
  displayName: string;
  disabled?: boolean;
  iconName: string;
  children?: NavItem[];
  expanded: boolean;
  depth: number;
  position: number[]; // position within the tree object

  constructor(displayName, iconName, depth, children?: NavItem[]) {
    this.displayName = displayName;
    this.iconName = iconName;
    this.expanded = false;
    this.depth = depth;
    this.children = children;
    this.position = [];
  }
}

