<template>
    <div>
        <h3>{{theme.title}}</h3>
        <ul>
            <li v-for="message in messages">
                {{message.content}}
            </li>
        </ul>
    </div>
</template>

<script>
    import theme from '../../store/modules/theme/theme';
    import actions from '../../store/modules/theme/action_types';
    import { mapGetters } from 'vuex';

    export default {
        props: ['id'],
        created() {
            const store = this.$store;

            if (!(store && store.state && store.state['theme'])) {
                store.registerModule('theme', theme);
            }
        },
        mounted() {
            var id = this.id;
            this.$store.dispatch(actions.GET_THEME, id);
            this.$store.dispatch(actions.GET_MESSAGES, { id, page: 0, pageSize: 10 });
        },
        destroyed() {
            this.$store.unregisterModule('theme');
        },
        computed: {
            ...mapGetters({
                theme: 'getTheme',
                messages: 'getMessages',
                pageOptions: 'getPageOptions'
            })
        }
    }
</script>