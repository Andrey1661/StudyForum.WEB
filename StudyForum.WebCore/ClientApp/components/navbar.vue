<template>
    <div>
        <nav class="navbar navbar-inverse">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">StudyForum</a>
                </div>
                <ul class="nav navbar-nav">
                    <li><a>Item1</a></li>
                    <li><a>Item2</a></li>
                    <li><a>Item3</a></li>
                </ul>

                <ul class="nav navbar-nav navbar-right">
                    <li v-if="!$store.getters.isLoggedIn"><router-link to="/login"><span class="glyphicon glyphicon-user"></span>Login</router-link></li>
                    <li v-if="$store.getters.isLoggedIn"><router-link to="/profile"><span>Профиль</span></router-link></li>
                    <li v-if="$store.getters.isLoggedIn"><a><span>Выйти</span></a></li>
                </ul>
            </div>
        </nav>
    </div>
</template>

<style>

</style>

<script>
    export default {

    }
</script>