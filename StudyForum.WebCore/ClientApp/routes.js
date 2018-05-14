const routes = [
    { name: 'login', path: "/account", component: require("./components/account.vue"), props: { inittype: 'login' } },
    {
        name: 'register',
        path: "/account",
        component: require("./components/account.vue"),
        props: { inittype: 'register' }
    },
    { path: '/login', redirect: { name: 'login' } },
    { path: '/register', redirect: { name: 'register' } },
    {
        path: '/',
        component: require("./components/layout.vue"),
        children: [
            { path: '/', component: require("./components/pages/home.vue") },
            { name: 'theme', path: '/theme/:id', component: require("./components/pages/themes.vue"), props: true },
            { path: '/profile', component: require("./components/pages/profile.vue") }
        ]
    }
];

export default routes;