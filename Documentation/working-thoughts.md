---
date: 2023-06-13
author: Spencer Farley
---


## Motivations

A music label called Recklass Rekkids (aka RR) wants to build a Global Rights Management (aka GRM) platform to allow them to best utilise their collection of music assets.

There are legal limitations in the ways in which RR can use the assets based on the contract signed with the artist. For example Monkey Claw agreed with RR to distribute his new song 'Motor Mouth' as a digital download starting 1st of Feb 2012, and as a streaming product starting from the 1st of March.

Agreements with distribution partners also have limitations. For example iTunes only sell digital downloads, while YouTube offers a streaming service.

SHORT: List all titles across artists that is allowed to be published on a given platform in a given timeframe. Platform compatibility is decided by content usage model of the platform (i.e. streaming, download) and what usage formats are allowed for that title

## Requirements

- REQ: Must be able to limit titles by availability start date
- REQ: Must be able to limit titles by an *optional* availability end date
- REQ: Must be able to specify a single usage format for a platform
- REQ: A title can have multiple usage models
- REQ: Must be able to limit titles by platform, where compatibility is decided by usage model of the platform and and what models are allowed for title


## Exploration


Q: What is my first increment?
- they've provided acceptance tests. I think I'll characterize the acceptance tests. 
- Then I'll handle degenerate cases, then look for a smallest increment to test

Acceptance tests
- Search for active music contracts (ITunes 03-01-2012)
  - limits to digital download
  - filters some downloads that are past the start date
  - start date is inclusive
- Search for active music contracts_2
  - just gets all streaming, no notable date behaviors
- Search for active music contracts_3
  - gets streaming and excludes some titles because start date is in the future


Q: should end date be inclusive?
- I'm going to assume yes for the purposes of the exercise

Q: How do I approach my developer tests
- I think I should use the provided example-based tests. I could make them property tests, but it seems right to use the given examples.
  - I might name the scenarios more uniquely, but comment with reference to each provided example
  - ALT: maybe I just used constrined indeterminism and create an explicit section of the provided example-based tests
- I can define scenarios outside the given examples as I like (i.e. for degenerate cases, etc)


Q: what are the main lines of effort I expect?
- Search capabilities
- parsing input files and getting them into search
  - the data is pipe-delimited. I'm sure I could find a parser
  - Q: Handle malformed input files gracefully? -> maybe come back to it
- validating/parsing console input

Definitely think I start with the meta search service, then move to mapping console information into it

Q: How do I test code between console and search service?
- I think the main behavior to test here is parsing the input files, which I can just test separately.

Q: They ask for BDD tests. Do they expect those to be full-stack integrated tests?
- I can probably do that but it'd take time to figure out all the piping...
- Let's finish other stuff before worrying about this

