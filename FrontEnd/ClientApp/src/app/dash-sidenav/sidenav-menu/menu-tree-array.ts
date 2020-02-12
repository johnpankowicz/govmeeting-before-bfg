import { NavItem } from './nav-item';
//import { navigationItems } from './menu-items';

export class MenuTreeArray {
  positions: number[];

  constructor() {
    this.positions = [];
  }

  // When a user chooses an item in the menu, we send the position of the item within
  // the tree to all code that needs to know which item was chosen. From it's positon,
  // they can then obtain everything they need to know.
  // Sending only the item object itself is not sufficient.
  // We store the depth as a seperate value to simpify styling.
  public assignPositions(items: NavItem[]) {
    let pos = 0;
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
}
