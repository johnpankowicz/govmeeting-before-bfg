import { NavItem } from './nav-item';
import { navigationItems } from './menu-items';

export class MenuTreeArray {

  positions: number[];
  //navItems: NavItem[];

  //constructor(_navigationItems: NavItem[]) {
    constructor() {
    this.positions = [];
    //this.navItems = navigationItems;
  }

/*
* Since a NavItem can have an array of child NavItems, it is a tree object.
* The sidenav menu consists of an array of NavItems (currently 2), each with their child NavItems..
* The 'position' property of a NavItem is its position within this array of trees.
* The position property is an array of numbers. (All counts start with 0.)
* [0]       = 1st NavItem in the sidenav array ("About")
* [1,2]     = 2nd NavItem's 3rd child. ("State of Maine")
* [1,3,1]   = 1st NavItem's 4th child's 2nd child. ("Senate" of United States)
*/
  // When a user chooses an item in the menu, we send the position of the item within
  // the menu tree to all code that needs to know which item was chosen. From it's positon,
  // they can then obtain everything they need to know.
  // Sending only the item object itself is not sufficient.
  // We store the depth as a seperate value to simpify styling.
  public assignPositions(items: NavItem[]) {
    //let items: NavItem[] = _items ? _items : this.navItems;
    let pos = 0;
    //for (var item of this.navItems) {
      for (var item of items) {
      //console.log("positions=" + this.positions)
      //console.log("pos=" + pos)
      if (this.positions.length >0) {
        for (var p of this.positions) {
          item.position.push(p);
        }
      }
      item.position.push(pos);
      if (item.children) {
        this.positions.push(pos);
        this.assignPositions(item.children)
        this.positions.pop();
      }
      pos = pos + 1;
    }
  }


  public getItem(position: number[], items: NavItem[]): NavItem {
    // If the first index in position array points to a childless item, that's the one.
    let item: NavItem = items[position[0]];
    if (!item.children) {
      return item;
    }
    // Remove the first index from the position array and do it again.
    let newPosition: number[] = position.slice(1);
    let selectedItem = this.getItem(newPosition, item.children);
    return selectedItem;
  }
}
