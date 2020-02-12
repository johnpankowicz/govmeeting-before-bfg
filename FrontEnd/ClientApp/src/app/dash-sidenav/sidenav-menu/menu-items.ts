import {NavItem} from './nav-item';

// Each menu item has: DisplayName, iconName, Depth, children array or route

let aboutpages = new NavItem('About', null, 0,
[
  new NavItem('Purpose', 'info', 1),
  new NavItem('Overview', 'toc', 1),
  new NavItem('Workflow', 'trending_up', 1),
  new NavItem('Auto Processing', 'directions_boat', 1),
  new NavItem('Manual Processing', 'rowing', 1),
  new NavItem('Extend Govmeeting', 'build', 1),
  new NavItem('[All Pages]', 'school', 1)
])

let boothbayharbor = new NavItem('Select Location', null, 0,
[
  new NavItem('Boothbay Harbor', 'location_city', 1, null),
  new NavItem('Lincoln County', 'landscape', 1, null),
  new NavItem('State of Maine', 'star', 1,
  [
    new NavItem('Senate', null, 2),
    new NavItem('House', null, 2)
  ]),
  new NavItem('United States', 'flag', 1,
  [
    new NavItem('Senate', null, 2),
    new NavItem('House', null, 2)
  ]),
  new NavItem('Non-Government', 'group', 1,
  [
    new NavItem('Glendale HOA', null, 2),
  ])
]);

// ];


export let austin = new NavItem('Select Location', null, 0,
[
  new NavItem('Austin', 'location_city', 1,
  [
    new NavItem('City Council', 'group', 2),
    new NavItem('Board of Education', 'school', 2),
    new NavItem('Planning Board', 'group', 2)
  ]),
  new NavItem('Travis County', 'group', 1),
  new NavItem('State of Texas', 'star', 1,
  [
    new NavItem('Senate', 'group', 2),
    new NavItem('House', 'group', 2)
  ]),
  new NavItem('United States', 'flag', 1,
  [
    new NavItem('Senate', 'group', 2),
    new NavItem('House', 'group', 2)
  ]),
  new NavItem('Non-Government', 'group', 1,
  [
    new NavItem('Glendale HOA', 'group', 2),
  ])
]);


export let navigationItems = [
  aboutpages,
  // change nxt line to "austin" use Austin.
  boothbayharbor
  // austin
]

