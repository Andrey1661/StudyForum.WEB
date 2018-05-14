<template>
    <div>
        <h2>Ваша группа: {{group.name}}</h2>
        <h3>Темы</h3>
        <ul>
            <li v-for="theme in themes">
                <span><router-link :to="{ name: 'theme', params: { id: theme.id } }">{{theme.title}}</router-link></span>
            </li>
        </ul>
        <h3>Предметы</h3>
        <ul>
            <li v-for="subject in subjects">
                <span>{{subject.name}}</span>
            </li>
        </ul>
    </div>
</template>

<script>
    import profile from '../../store/modules/profile/profile';
    import * as actions from '../../store/modules/profile/action_types';
    import { mapGetters } from 'vuex';

    export default {
        data() {
            return { }
        },
        created() {
            const store = this.$store;

            if (!(store && store.state && store.state['profile'])) {
                store.registerModule('profile', profile);
            }
        },
        mounted() {
            this.$store.dispatch(actions.GET_PROFILE);
        },
        destroyed() {
            this.$store.unregisterModule('profile');
        },
        computed: {
            ...mapGetters([
                'subjects',
                'group',
                'themes'
            ])
        }
    }
</script>

<style>

</style>