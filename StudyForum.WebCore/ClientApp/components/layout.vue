<template>
    <div>
        <navbar></navbar>
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
    import navbar from "./navbar.vue"

    export default {
        components: {
            'navbar': navbar
        }
    }
</script>

<style>
    html, body {
        height: 100%;
    }

    body {
        background: linear-gradient(to bottom, #f4f4f4, #ffffff)
    }
</style>
