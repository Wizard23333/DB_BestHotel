import Vue from 'vue'
import Router from 'vue-router'
import Login from '../components/Login.vue'
import dishTable from '../components/dishTable.vue'
import welcome from '../components/welcome.vue'
import orderTable from '../components/orderTable'
import information from '../components/information'
import home from '../components/home'
import home_cli from '../components/home_cli'
import goodsRoom from '../components/goods/room_goods/room_info.vue'
import goodsDish from '../components/goods/dish_goods/index.vue'
import facility from '../components/facility.vue'
import Orderroom from '../components/order/Orderroom.vue'
import Orderdish from '../components/order/Orderdish.vue'
import Parking from '../components/parking/Parking.vue'
import roomAdd from '../components/room/add.vue'
import roomList from '../components/room/list.vue'
import roomType from '../components/room/type.vue'
import dishAdd from '../components/dish/add.vue'
import dishList from '../components/dish/list.vue'
import moni from '../components/moni.vue'
import StaffManage from '../components/StaffManage.vue'
import personal from '../components/personal.vue'
//import index from '../components/index'

Vue.use(Router)
// export default new Router({
//   routes:[
//  // {path:'/orderTable',component:orderTable},
//   {path:'/home',component:home
// ,children:[//{path:'/orderTable',component:orderTable},
// // {path:'/dishTable',component:dishTable},
// // {path:'/goodsRoom',component:goodsRoom},
// // {path: '/facility',component: facility},
// // {path: '/goodsDish',component: goodsDish},
// {path:'/moni',component:moni},
// { path: '/orderroom', component: Orderroom },
// {path:'/StaffManage',component:StaffManage},
// { path: '/orderdish', component: Orderdish },
// { path: '/parking', component: Parking },
// { path: '/roomAdd', component: roomAdd },
// { path: '/roomList', component: roomList },
// { path: '/roomType', component: roomType },
// { path: '/dishAdd', component: dishAdd },
// { path: '/dishList', component: dishList },
// {path:'/facility',component:facility},
// { path: '/welcome1', component: welcome },
// { path: '/personal', component: personal },
// ]},
// {path:'/home_cli',component:home_cli
// ,children:[{path:'/orderTable',component:orderTable},
// // {path:'/dishTable',component:dishTable},
//  {path:'/goodsRoom',component:goodsRoom},
// // {path: '/facility',component: facility},
//  {path: '/goodsDish',component: goodsDish},

// {path:'/information',component:information},
// { path: '/welcome', component: welcome },

// { path: '/dishTable', component: dishTable },
// ]}
//   ,
//   //{path:'/index',component:index},
//   {path:'/',redirect: 'Login'},
//   {path: '/Login',component: Login},
//   {path: '/dishTable',component: dishTable},
//   {path: '/welcome',component: welcome},
//   {path: '/information',component: information}
//   //{path: '/Login',redirect:'home'},
//   // {path:'/home',
//   // component:home,
//   // redirect:'/welcome',
//   // children:[{ path:'/welcome',component:welcome}
//   // ]}
//   ]
// })

const routes=[
  {path:'/home',component:home
,children:[
{path:'/moni',component:moni},
{ path: '/orderroom', component: Orderroom },
{path:'/StaffManage',component:StaffManage},
{ path: '/orderdish', component: Orderdish },
{ path: '/parking', component: Parking },
{ path: '/roomAdd', component: roomAdd },
{ path: '/roomList', component: roomList },
{ path: '/roomType', component: roomType },
{ path: '/dishAdd', component: dishAdd },
{ path: '/dishList', component: dishList },
{path:'/facility',component:facility},
{ path: '/welcome1', component: welcome },
{ path: '/personal', component: personal },
]},
{path:'/home_cli',component:home_cli
,children:[{path:'/orderTable',component:orderTable},

 {path:'/goodsRoom',component:goodsRoom},

 {path: '/goodsDish',component: goodsDish},

{path:'/information',component:information},
{ path: '/welcome', component: welcome },

{ path: '/dishTable', component: dishTable },
]}
  ,
  {path:'/',redirect: 'Login'},
  {path: '/Login',component: Login},
  {path: '/dishTable',component: dishTable},
  {path: '/welcome',component: welcome},
  {path: '/information',component: information}

  ]
  const router = new Router({
    routes
  })

  router.beforeEach((to, from, next) => {
    // 访问登录页，放行
    if (to.path === '/login') return next()
    // 获取token
    const tokenStr = window.sessionStorage.getItem('user_id')
    // 没有token, 强制跳转到登录页
    if (!tokenStr) return next('/login')
    next()
  })
  
  export default router