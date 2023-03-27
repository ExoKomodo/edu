import { describe, it, expect } from 'vitest'

import { shallowMount } from '@vue/test-utils'
import Navbar from '../Navbar.vue'

describe('Navbar', () => {
  it('renders properly', () => {
    const wrapper = shallowMount(Navbar);
    expect(wrapper.text()).toContain('Edu');
  })
})

export {}
