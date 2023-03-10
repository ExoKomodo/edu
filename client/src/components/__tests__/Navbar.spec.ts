import { describe, it, expect } from 'vitest'

import { mount } from '@vue/test-utils'
import Navbar from '../Navbar.vue'

describe('HelloWorld', () => {
  it('renders properly', () => {
    const wrapper = mount(Navbar)
    expect(wrapper.text()).toContain('Edu')
  })
})
// TODO - why are we using vitest AND cypress?
export {}